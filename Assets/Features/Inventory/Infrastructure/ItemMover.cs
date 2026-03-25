using Feature.Items.Infrastructure;
using Shared.Service;
using UnityEngine;

namespace Feature.Inventory.Infrastructure
{
    public class ItemMover : IItemMover
    {
        private readonly IWorldPhysicsService _worldPhysicsService;
        private readonly IDelay _delayService;

        public ItemMover(IWorldPhysicsService worldPhysicsService, IDelay delayService)
        {
            _worldPhysicsService = worldPhysicsService;
            _delayService = delayService;
        }

        public void PickupItem(GameObject obj, Vector3? displayOffset)
        {
            _worldPhysicsService.ConfigureRigidbodyMode(obj, isPickup: true);
            _worldPhysicsService.ToggleCollider(obj, false);
            _worldPhysicsService.AttachToParent(obj, ParentTarget.Hand);

            _worldPhysicsService.SetOffset(obj, displayOffset ?? Vector3.zero);
        }

        public void MoveToInventory(GameObject obj)
        {
            _worldPhysicsService.AttachToParent(obj, ParentTarget.Inventory);
        }

        public void MoveToHand(GameObject obj)
        {
            _worldPhysicsService.AttachToParent(obj, ParentTarget.Hand);
        }

        public void DropItem(GameObject obj, Vector3 dropPosition, Quaternion dropRotation)
        {
            _worldPhysicsService.MoveObject(obj, dropPosition, dropRotation);
            _worldPhysicsService.ToggleCollider(obj, true);
            _worldPhysicsService.AttachToParent(obj, ParentTarget.World);

            _delayService.ExecuteAfterDelay(0.01f, () => _worldPhysicsService.ConfigureRigidbodyMode(obj, isPickup: false));
        }
    }
}