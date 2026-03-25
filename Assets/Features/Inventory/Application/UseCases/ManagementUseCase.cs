using Feature.Inventory.Data;
using Feature.Inventory.Domain;
using Shared.Data;
using Shared.Providers;
using System;

namespace Feature.Inventory.Application
{
    public class ManagementUseCase : IManagementUseCase
    {
        private readonly IPlayerInventory _playerInventory;
        private readonly ITimeProvider _time;
        private readonly ICooldownService _cooldown;

        public ManagementUseCase(IPlayerInventory playerInventory, ITimeProvider time, ICooldownService cooldown)
        {
            _playerInventory = playerInventory;
            _time = time;
            _cooldown = cooldown;
        }

        /// <summary>
        /// Attempts to add an item to the player's inventory using the specified item context. Returns a result indicating
        /// success or failure.
        /// </summary>
        /// <remarks>This method will fail if the inventory is not available at the current time. The returned
        /// result should be checked for success before accessing the inventory item.</remarks>
        /// <param name="itemContext">The context information for the item to be added, including its configuration and world identifiers. Cannot be
        /// null.</param>
        /// <returns>A result containing the added inventory item if the operation succeeds; otherwise, a failure result with an
        /// error message.</returns>
        public Result<InventoryItem> TryAddItem(ItemContext itemContext)
        {
            var result = _playerInventory.Add(itemContext.ConfigId, itemContext.WorldId);
            if (!result.IsSuccess)
                return Result<InventoryItem>.Failure("Failed to add item to inventory");

            return Result<InventoryItem>.Success(result.Value);
        }

        /// <summary>
        /// Attempts to select the specified inventory slot and returns a snapshot of the item selection state.
        /// </summary>
        /// <remarks>If the specified slot is successfully selected, the returned snapshot includes information
        /// about the previously selected slot and item, as well as the newly selected item. The method does not modify the
        /// inventory if it is unavailable at the time of the call.</remarks>
        /// <param name="slot">The zero-based index of the inventory slot to select. Must refer to a valid slot within the player's inventory.</param>
        /// <returns>A result containing an item selection snapshot that reflects the state before and after the slot selection. If
        /// the inventory is not available, the result indicates failure with an appropriate error message.</returns>
        public Result<ItemSelectionSnapshot> TrySelectItem(int slot, float equipDuration)
        {
            int previousSlotId = _playerInventory.CurrentSlot;

            var result = _playerInventory.SelectSlot(slot);
            if (!result.IsSuccess)
                return Result<ItemSelectionSnapshot>.Failure("Failed to select inventory slot");

            var previousSelectedItem = _playerInventory.GetBySlot(previousSlotId);
            var currentSelectedItem = _playerInventory.GetSelectedItem();

            var previousWorldId = previousSelectedItem != null ? previousSelectedItem.WorldId : (Guid?)null;
            var currentWorldId = currentSelectedItem != null ? currentSelectedItem.WorldId : (Guid?)null;
            var previousConfigId = previousSelectedItem != null ? previousSelectedItem.ConfigId : (int?)null;
            var currentConfigId = currentSelectedItem != null ? currentSelectedItem.ConfigId : (int?)null;

            var snapshot = new ItemSelectionSnapshot(
                previousWorldId,
                currentWorldId,
                previousSlotId,
                slot,
                previousConfigId,
                currentConfigId);

            _cooldown.UpdateLastUseTime(_time.Now, equipDuration);

            return Result<ItemSelectionSnapshot>.Success(snapshot);
        }

        /// <summary>
        /// Attempts to delete the currently selected item from the player's inventory.
        /// </summary>
        /// <remarks>This method will fail if the inventory is not available or if no item is currently selected.
        /// The deleted item is returned in the result upon success.</remarks>
        /// <returns>A <see cref="Result{InventoryItem}"/> indicating the outcome of the operation. If successful, contains the
        /// deleted item; otherwise, contains an error message describing the failure.</returns>
        public Result<InventoryItem> TryDeleteItem()
        {
            var currentSelectedItem = _playerInventory.GetSelectedItem();
            if (currentSelectedItem == null)
                return Result<InventoryItem>.Failure("No item selected to drop");

            _playerInventory.Delete(currentSelectedItem);

            return Result<InventoryItem>.Success(currentSelectedItem);

        }

    }
}