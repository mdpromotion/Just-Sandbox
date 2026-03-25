using System;
using UnityEngine;

namespace Shared.Data
{
    public readonly struct ItemContext
    {
        public GameObject Object { get; }
        public int ConfigId { get; }
        public Guid WorldId { get; }
        public ItemContext(GameObject obj, int configId, Guid worldId)
        {
            Object = obj;
            ConfigId = configId;
            WorldId = worldId;
        }
    }
}