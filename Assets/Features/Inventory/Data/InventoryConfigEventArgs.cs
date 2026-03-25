using System;

namespace Feature.Inventory.Data
{
    public readonly struct InventoryConfigEventArgs
    {
        public int SlotId { get; }
        public int? ConfigId { get; }
        public Guid? WorldId { get; }
        public float EquipDuration { get; }

        public InventoryConfigEventArgs(int slotId, int? configId, Guid? worldId, float equipDuration)
        {
            SlotId = slotId;
            ConfigId = configId;
            WorldId = worldId;
            EquipDuration = equipDuration;
        }
    }
}