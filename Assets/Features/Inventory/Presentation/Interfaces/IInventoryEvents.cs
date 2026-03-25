using Feature.Inventory.Data;
using System;

namespace Feature.Inventory.Application
{
    public interface IInventoryEvents
    {
        event Action<InventoryConfigEventArgs> SlotSelected;
        event Action<InventoryConfigEventArgs> SlotUnselected;
    }
}