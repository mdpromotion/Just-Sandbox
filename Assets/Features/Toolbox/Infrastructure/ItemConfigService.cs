using Feature.Combat.Infrastructure;
using Feature.Items.Infrastructure;

namespace Feature.Toolbox.Infrastructure
{
    public class ItemConfigService : IItemConfigService
    {
        private readonly ItemDatabase _itemDatabase;

        public ItemConfigService(ItemDatabase database)
        {
            _itemDatabase = database;
        }

        public Result<IItemProvider> GetItemConfig(int id)
        {
            var item = _itemDatabase.GetItemById(id);
            if (!item.IsSuccess) return Result<IItemProvider>.Failure(item.Error);

            return item.Value switch
            {
                WeaponData weapon => Result<IItemProvider>.Success(new WeaponProvider(weapon)),
                ItemData generic => Result<IItemProvider>.Success(new ItemProvider(generic)),
                _ => Result<IItemProvider>.Failure($"Unsupported item type: {item.Value.GetType()}")
            };
        }
    }
}