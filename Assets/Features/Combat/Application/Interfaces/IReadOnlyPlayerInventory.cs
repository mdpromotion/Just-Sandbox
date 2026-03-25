using System;

namespace Feature.Inventory.Domain
{
    public interface IReadOnlyPlayerInventory
    {
        Guid GetSelectedWorldId();
    }
}