using Feature.Items.Domain;
using UnityEngine;

namespace Feature.Items.Infrastructure
{
    public class WorldItemBind : MonoBehaviour
    {
        private Renderer _renderer = null;

        public WorldItem WorldItem { get; private set; }

        public void Bind(WorldItem worldItem, Material material = null)
        {
            WorldItem = worldItem;

            if (material != null)
            {
                _renderer = GetComponentInChildren<Renderer>();
                SetMaterial(material);
            }
        }

        private void SetMaterial(Material material)
        {
            if (_renderer == null)
                _renderer = GetComponentInChildren<Renderer>();

            _renderer.material = material;
        }
    }
}