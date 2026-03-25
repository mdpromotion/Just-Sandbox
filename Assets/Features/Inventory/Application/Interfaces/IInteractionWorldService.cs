using Shared.Data;

namespace Feature.Inventory.Infrastructure
{
    public interface IInteractionWorldService
    {
        ItemContext? TryGetItem();
    }
}