#nullable enable
using System;

namespace Feature.Inventory.Domain
{
    public interface IPlayerInventory
    {
        public int CurrentSlot { get; }
        Result<InventoryItem> Add(int configId, Guid worldId, int? preferredSlot = null);
        void Delete(InventoryItem item);
        Result SelectSlot(int slotId);
        int? GetFreeSlot();
        InventoryItem? GetSelectedItem();
        InventoryItem? GetById(Guid id);
        InventoryItem? GetBySlot(int slot);
    }
}
