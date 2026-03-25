using UnityEngine;

namespace Feature.Inventory.Infrastructure
{
    public interface IItemMover
    {
        void PickupItem(GameObject obj, Vector3? displayOffset);
        void MoveToInventory(GameObject obj);
        void MoveToHand(GameObject obj);
        void DropItem(GameObject obj, Vector3 dropPosition, Quaternion dropRotation);
    }
}