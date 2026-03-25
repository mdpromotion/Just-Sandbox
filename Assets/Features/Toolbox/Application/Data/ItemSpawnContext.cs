using Feature.Items.Infrastructure;
using UnityEngine;

namespace Feature.Toolbox.Data
{
    public readonly struct ItemSpawnContext
    {
        public GameObject Object { get; }
        public Material Material { get; }
        public IItemProvider Config { get; }
        public ItemSpawnContext(GameObject obj, Material material, IItemProvider config)
        {
            Object = obj;
            Material = material;
            Config = config;
        }
    }
}