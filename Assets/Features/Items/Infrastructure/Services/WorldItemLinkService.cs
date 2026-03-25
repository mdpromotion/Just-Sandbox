using Feature.Items.Domain;
using Shared.Service;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Items.Infrastructure
{
    public class WorldItemLinkService : IWorldItemLinkService
    {
        private readonly Dictionary<WorldItem, GameObject> _map = new();

        public bool TryGetGameObject(WorldItem item, out GameObject go)
        {
            return _map.TryGetValue(item, out go);
        }

        public void Bind(WorldItem item, GameObject go)
        {
            _map[item] = go;
        }
        public void Unbind(WorldItem item)
        {
            _map.Remove(item);
        }
    }
}