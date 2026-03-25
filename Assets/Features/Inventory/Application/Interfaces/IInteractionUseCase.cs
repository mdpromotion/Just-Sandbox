
using Shared.Data;
using System;

namespace Feature.Inventory.Application
{
    public interface IInteractionUseCase
    {
        Result<ItemContext> TryGetItemFromWorld();
        Result TryPickupItem(Guid currentWorldId, int configId);
        Result TrySelectItem(Guid? previousWorldId, Guid? currentWorldId, bool hasItemInSlot);
        Result TryDropItem(Guid currentWorldId);
    }
}