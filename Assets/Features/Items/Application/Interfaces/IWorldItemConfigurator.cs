using Feature.Items.Domain;
using UnityEngine;

namespace Feature.Items.Infrastructure
{
    public interface IWorldItemConfigurator
    {
        Result Configure(GameObject go, WorldItem item, Material material = null);
    }
}