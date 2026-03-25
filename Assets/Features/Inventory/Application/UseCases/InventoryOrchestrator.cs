using Feature.Inventory.Data;
using Shared.Data;
using System;
using UnityEngine;

namespace Feature.Inventory.Application
{
    public class InventoryOrchestrator : IInventoryPickupInput, IInventoryEvents
    {
        public readonly static string LogTag = nameof(InventoryOrchestrator);
        private readonly IManagementUseCase _inventoryManagmentUseCase;
        private readonly IInteractionUseCase _inventoryInteractionUseCase;
        private readonly ILogger _logger;

        private readonly float EquipDuration;

        public event Action<InventoryConfigEventArgs> SlotSelected;
        public event Action<InventoryConfigEventArgs> SlotUnselected;

        public InventoryOrchestrator(
            IManagementUseCase inventoryManagmentUseCase,
            IInteractionUseCase inventoryInteractionUseCase,
            ILogger logger,
            float equipDuration = 0.9f)
        {
            _inventoryManagmentUseCase = inventoryManagmentUseCase;
            _inventoryInteractionUseCase = inventoryInteractionUseCase;
            _logger = logger;
            EquipDuration = equipDuration;
        }

        /// <summary>
        /// Attempts to pick up an item from the world and add it to the inventory. Logs an error if the operation is
        /// unsuccessful.
        /// </summary>
        /// <remarks>This method performs no action if no item is available to pick up or if the pickup
        /// operation fails. Errors encountered during the process are logged for diagnostic purposes.</remarks>
        public bool TryPickupItem()
        {
            var getItemResult = _inventoryInteractionUseCase.TryGetItemFromWorld();
            if (!getItemResult.IsSuccess)
            {
                _logger.Log(LogTag, getItemResult.Error);
                return false;
            }

            var result = TryPickupSpawnedItem(getItemResult.Value);
            if (!result.IsSuccess)
            {
                _logger.Log(LogTag, result.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Attempts to pick up a spawned item and add it to the inventory. Returns a result indicating whether the
        /// operation was successful.
        /// </summary>
        /// <remarks>If the item cannot be added to the inventory or picked up, the operation fails and
        /// the error is provided in the result. The item is automatically selected in the inventory upon successful
        /// pickup.</remarks>
        /// <param name="item">The context of the item to be picked up, including its world identifier and configuration. Cannot be null.</param>
        /// <returns>A result indicating success if the item was picked up and added to the inventory; otherwise, a failure
        /// result containing the error.</returns>
        public Result<int> TryPickupSpawnedItem(ItemContext item)
        {
            var addResult = _inventoryManagmentUseCase.TryAddItem(item);
            if (!addResult.IsSuccess)
            {
                return Result<int>.Failure(addResult.Error);
            }

            var pickupResult = _inventoryInteractionUseCase.TryPickupItem(item.WorldId, item.ConfigId);

            if (!pickupResult.IsSuccess)
            {
                return Result<int>.Failure(pickupResult.Error);
            }

            TrySelectItem(addResult.Value.SlotId, true);

            return Result<int>.Success(addResult.Value.SlotId);
        }

        /// <summary>
        /// Attempts to select the item in the specified inventory slot and triggers the item selection event if
        /// successful.
        /// </summary>
        /// <remarks>If the selection fails due to an invalid slot or other error, an error is logged and
        /// no event is triggered.</remarks>
        /// <param name="slot">The zero-based index of the inventory slot to select.</param>
        public bool TrySelectItem(int slot, bool force = false)
        {
            float equipDuration = force ? 0f : EquipDuration;

            var snapshotResult = _inventoryManagmentUseCase.TrySelectItem(slot, equipDuration);
            if (!snapshotResult.IsSuccess)
            {
                _logger.LogError(LogTag, snapshotResult.Error);
                return false;
            }

            var snapshot = snapshotResult.Value;

            ItemUnselectEvent(snapshot.PreviousSlotId, snapshot.PreviousConfigId);

            var selectInteractionResult = _inventoryInteractionUseCase.TrySelectItem(
                snapshot.PreviousWorldId,
                snapshot.CurrentWorldId,
                snapshot.HasItemInSlot);

            if (!selectInteractionResult.IsSuccess)
            {
                _logger.LogError(LogTag, selectInteractionResult.Error);
                return false;
            }

            ItemSelectEvent(slot, snapshot.CurrentConfigId, snapshot.CurrentWorldId);
            return true;
        }

        /// <summary>
        /// Attempts to remove the currently selected item from the inventory and drop it into the game world. Logs a
        /// warning or error if the operation fails at any stage.
        /// </summary>
        /// <remarks>If the item cannot be deleted from the inventory or dropped into the world, a warning
        /// or error is logged and no further action is taken. This method does not throw exceptions for failure cases;
        /// instead, it relies on logging for error reporting.</remarks>
        public bool TryDropItem()
        {
            var itemResult = _inventoryManagmentUseCase.TryDeleteItem();
            if (!itemResult.IsSuccess)
            {
                _logger.LogWarning(LogTag, itemResult.Error);
                return false;
            }

            var item = itemResult.Value;

            var dropInteractionResult = _inventoryInteractionUseCase.TryDropItem(item.WorldId);
            if (!dropInteractionResult.IsSuccess)
            {
                _logger.LogError(LogTag, dropInteractionResult.Error);
                return false;
            }

            ItemSelectEvent(itemResult.Value.SlotId);
            return true;
        }

        private void ItemSelectEvent(int slotId, int? configId = null, Guid? worldId = null)
        {
            SlotSelected?.Invoke(new InventoryConfigEventArgs(slotId, configId, worldId, EquipDuration));
        }
        private void ItemUnselectEvent(int slotId, int? configId = null, Guid? worldId = null)
        {
            SlotUnselected?.Invoke(new InventoryConfigEventArgs(slotId, configId, worldId, EquipDuration));
        }
    }
}