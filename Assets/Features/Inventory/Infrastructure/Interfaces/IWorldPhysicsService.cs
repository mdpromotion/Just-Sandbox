using UnityEngine;

namespace Feature.Items.Infrastructure
{
    public interface IWorldPhysicsService
    {
        void MoveObject(GameObject obj, Vector3 position, Quaternion rotation);
        void ToggleObject(GameObject gameObject, bool state);
        void ToggleCollider(GameObject obj, bool state);
        void SelectLayer(GameObject obj, int layerIndex);

        /// <summary>
        /// Attaches the specified game object to a parent target, applying the given positional offset.
        /// </summary>
        /// <param name="obj">The game object to attach to the parent. Cannot be null.</param>
        /// <param name="offset">The positional offset to apply when attaching the object, relative to the parent target.</param>
        /// <param name="target">The parent target to which the game object will be attached.</param>
        void AttachToParent(GameObject obj, ParentTarget target);

        /// <summary>
        /// Configures the rigidbody mode of the specified GameObject for either pickup or standard interaction.
        /// </summary>
        /// <param name="obj">The GameObject whose rigidbody mode will be configured. Cannot be null.</param>
        /// <param name="isPickup">A value indicating whether the GameObject should be set up for pickup interaction. If <see langword="true"/>,
        /// the rigidbody is configured for pickup; otherwise, it is configured for standard interaction.</param>
        /// <returns>true if the rigidbody mode was successfully configured; otherwise, false.</returns>
        bool ConfigureRigidbodyMode(GameObject obj, bool isPickup);
        void SetOffset(GameObject obj, Vector3 offset);
    }
}
