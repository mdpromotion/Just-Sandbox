using Feature.Inventory.Infrastructure;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Feature.Items.Infrastructure
{
    public enum ParentTarget
    {
        Hand,
        Inventory,
        World
    }
    public sealed class WorldPhysicsService : IWorldPhysicsService
    {
        private readonly Transform _handParent;
        private readonly Transform _inventoryParent;

        public WorldPhysicsService(
            [Inject(Id = "HandParent")] Transform handParent,
            [Inject(Id = "InventoryParent")] Transform inventoryParent
            )
        {
            _handParent = handParent;
            _inventoryParent = inventoryParent;
        }
        public bool ConfigureRigidbodyMode(GameObject obj, bool isPickup)
        {
            if (!obj.TryGetComponent<Rigidbody>(out var rb))
                return false;

            if (isPickup)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
                rb.interpolation = RigidbodyInterpolation.None;
            }
            else
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.interpolation = RigidbodyInterpolation.Interpolate;
            }

            return true;
        }

        public void MoveObject(GameObject obj, Vector3 position, Quaternion rotation)
        {
            obj.transform.SetPositionAndRotation(position, rotation);
        }

        public void ToggleObject(GameObject gameObject, bool state)
        {
            gameObject.SetActive(state);
        }

        public void ToggleCollider(GameObject obj, bool state)
        {
            var collider = obj.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = state;
            }
        }

        public void SelectLayer(GameObject obj, int layerIndex)
        {
            obj.layer = layerIndex;
        }

        public void AttachToParent(GameObject obj, ParentTarget target)
        {
            Transform parent = target switch
            {
                ParentTarget.Hand => _handParent,
                ParentTarget.Inventory => _inventoryParent,
                ParentTarget.World => null,
                _ => obj.transform.parent
            };

            Vector3 previousScale = obj.transform.localScale;

            obj.transform.SetParent(parent);

            obj.transform.localRotation = Quaternion.Euler(0,0,0);

            obj.transform.localScale = previousScale;
        }

        public void SetOffset(GameObject obj, Vector3 offset)
        {
            obj.transform.localPosition = offset;
        }

    }
}