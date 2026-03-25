using Feature.Inventory.Data;
using Feature.Inventory.Domain;
using Shared.Data;

namespace Feature.Inventory.Application
{
    public interface IManagementUseCase
    {
        Result<InventoryItem> TryAddItem(ItemContext itemContext);
        Result<ItemSelectionSnapshot> TrySelectItem(int slot, float equipDuration);
        Result<InventoryItem> TryDeleteItem();
    }
}