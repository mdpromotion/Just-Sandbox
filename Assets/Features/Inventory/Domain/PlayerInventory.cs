#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feature.Inventory.Domain
{
    public class PlayerInventory : IReadOnlyPlayerInventory, IPlayerInventory
    {
        private readonly List<InventoryItem> _inventoryItems;
        public IReadOnlyList<InventoryItem> Items => _inventoryItems.AsReadOnly();
        private readonly int _maxSlots;
        public int CurrentSlot { get; private set; }

        public PlayerInventory(int maxSlots = 7)
        {
            _maxSlots = maxSlots;
            _inventoryItems = new List<InventoryItem>();
            CurrentSlot = -1;
        }

        public Result<InventoryItem> Add(int configId, Guid worldId, int? preferredSlot = null)
        {
            if (_inventoryItems.Count >= _maxSlots)
                return Result<InventoryItem>.Failure("Inventory is full.");

            var freeSlot = preferredSlot ?? GetFreeSlot();
            if (!freeSlot.HasValue || IsSlotOccupied(freeSlot.Value))
                return Result<InventoryItem>.Failure("No free slots available.");

            if (ContainsItem(worldId))
                return Result<InventoryItem>.Failure("Item already exists in inventory.");

            var item = new InventoryItem(Guid.NewGuid(), freeSlot.Value, configId, worldId);

            _inventoryItems.Add(item);
            return Result<InventoryItem>.Success(item);
        }

        public void Delete(InventoryItem item)
        {
            _inventoryItems.Remove(item);
        }

        public Result SelectSlot(int slotId)
        {
            if (slotId < 0 || slotId >= _maxSlots)
                return Result.Failure("Invalid slot ID.");

            CurrentSlot = slotId;
            return Result.Success();
        }

        public int? GetFreeSlot()
        {
            for (int i = 0; i < _maxSlots; i++)
            {
                if (_inventoryItems.All(item => item.SlotId != i))
                    return i;
            }
            return null;
        }

        public Guid GetSelectedWorldId()
        {
            var selectedItem = GetSelectedItem();
            return selectedItem?.WorldId ?? Guid.Empty;
        }

        public InventoryItem? GetSelectedItem()
        {
            return _inventoryItems.FirstOrDefault(item => item.SlotId == CurrentSlot);
        }

        public InventoryItem? GetById(Guid id)
        {
            return _inventoryItems.FirstOrDefault(item => item.Id == id);
        }

        public InventoryItem? GetBySlot(int slot)
        {
            return _inventoryItems.FirstOrDefault(item => item.SlotId == slot);
        }

        private bool IsSlotOccupied(int slotId)
        {
            return _inventoryItems.Any(item => item.SlotId == slotId);
        }

        private bool ContainsItem(Guid worldId)
        {
            return _inventoryItems.Any(item => item.WorldId == worldId);
        }
    }
}