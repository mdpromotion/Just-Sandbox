using Feature.Items.Domain;
using UnityEngine;

namespace Shared.Service
{
    public interface IWorldItemLinkService
    {
        bool TryGetGameObject(WorldItem item, out GameObject go);
        void Bind(WorldItem item, GameObject go);
        void Unbind(WorldItem item);
    }
}