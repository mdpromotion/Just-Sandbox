using System;

namespace Feature.Inventory.Data
{
    public class ItemSelectionSnapshot
    {
        public Guid? PreviousWorldId { get; }
        public Guid? CurrentWorldId { get; }
        public int PreviousSlotId { get; }
        public int CurrentSlotId { get; }
        public int? PreviousConfigId { get; }
        public int? CurrentConfigId { get; }
        public bool HasItemInSlot => CurrentConfigId.HasValue;

        public ItemSelectionSnapshot(
            Guid? previousWorldId,
            Guid? currentWorldId,
            int previousSlotId,
            int currentSlotId,
            int? previousConfigId,
            int? currentConfigId)
        {
            PreviousWorldId = previousWorldId;
            CurrentWorldId = currentWorldId;
            PreviousSlotId = previousSlotId;
            CurrentSlotId = currentSlotId;
            PreviousConfigId = previousConfigId;
            CurrentConfigId = currentConfigId;
        }
    }
}