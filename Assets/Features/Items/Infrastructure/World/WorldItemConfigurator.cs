using Feature.Items.Domain;
using UnityEngine;

namespace Feature.Items.Infrastructure
{
    public class WorldItemConfigurator : IWorldItemConfigurator
    {
        public Result Configure(GameObject go, WorldItem item, Material material = null)
        {
            if (go.TryGetComponent(out WorldItemBind view))
            {
                view.Bind(item, material);
                return Result.Success();
            }
            else
            {
                return Result.Failure($"GameObject {go.name} does not have a WorldItemBind component.");
            }
        }
    }
}