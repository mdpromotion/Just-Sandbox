using System;

namespace Feature.Inventory.Domain
{
    public class InventoryItem
    {
        public Guid Id { get; }
        public int SlotId { get; }
        public int ConfigId { get; }
        public Guid WorldId { get; }

        public InventoryItem(Guid id, int slotId, int configId, Guid worldId)
        {
            Id = id;
            SlotId = slotId;
            ConfigId = configId;
            WorldId = worldId;
        }
    }
}