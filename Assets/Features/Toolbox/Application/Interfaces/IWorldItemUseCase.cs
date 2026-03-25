using Feature.Items.Infrastructure;
using Shared.Data;
using UnityEngine;

namespace Feature.Items.Application
{
    public interface IWorldItemUseCase
    {
        Result<ItemContext> SpawnItem(IItemProvider item, GameObject obj, Material material = null);
    }
}