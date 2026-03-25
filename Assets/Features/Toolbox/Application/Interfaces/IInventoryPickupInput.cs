using Shared.Data;

namespace Feature.Inventory.Application
{
    public interface IInventoryPickupInput
    {
        /// <summary>
        /// Attempts to pick up an item if one is available and the current conditions allow it.
        /// </summary>
        bool TryPickupItem();
        /// <summary>
        /// Attempts to pick up a spawned item from the specified context.
        /// </summary>
        /// <remarks>This method may fail if the item is not available for pickup or if the operation is
        /// not permitted in the current state.</remarks>
        /// <param name="item">The context of the item to be picked up. This parameter must not be null.</param>
        /// <returns>A result containing the identifier of the picked-up item if the operation succeeds; otherwise, an error code
        /// indicating the reason for failure.</returns>
        Result<int> TryPickupSpawnedItem(ItemContext item);

        /// <summary>
        /// Attempts to select the item in the specified slot, optionally forcing the selection even if the item is
        /// already selected.
        /// </summary>
        /// <remarks>If the specified slot is invalid, the method performs no action. Forcing the
        /// selection may trigger additional state changes, even if the item is already selected.</remarks>
        /// <param name="slot">The zero-based index of the slot to select. Must be a non-negative integer representing a valid slot
        /// position.</param>
        /// <param name="force">true to force the selection even if the item is already selected; otherwise, false.</param>
        bool TrySelectItem(int slot, bool force = false);
        /// <summary>
        /// Attempts to drop the currently held item, if any.
        /// </summary>
        bool TryDropItem();
    }
}