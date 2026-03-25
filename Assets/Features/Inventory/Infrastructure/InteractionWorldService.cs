using Feature.Items.Infrastructure;
using Shared.Data;
using UnityEngine;

namespace Feature.Inventory.Infrastructure
{
    public class InteractionWorldService : IInteractionWorldService
    {
        private readonly Camera _camera;
        private readonly float _interactionRange = 5f;
        private readonly LayerMask _layerMask;

        public InteractionWorldService(
            Camera camera,
            float interactionRange = 5f,
            LayerMask? layerMask = null)
        {
            _camera = camera;
            _interactionRange = interactionRange;
            _layerMask = layerMask ?? LayerMask.GetMask("Interactable");
        }

        public ItemContext? TryGetItem()
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _interactionRange, _layerMask))
            {
                if (hit.collider.TryGetComponent<WorldItemBind>(out var binder))
                {
                    var obj = binder.gameObject;
                    if (obj != null)
                        return new ItemContext(obj, binder.WorldItem.ConfigId, binder.WorldItem.Id);
                }
            }
            return null;
        }
    }
}