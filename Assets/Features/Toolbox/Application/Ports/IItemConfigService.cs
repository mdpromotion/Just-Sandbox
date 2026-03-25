using Feature.Items.Infrastructure;

namespace Feature.Toolbox.Infrastructure
{
    public interface IItemConfigService
    {
        Result<IItemProvider> GetItemConfig(int id);
    }
}