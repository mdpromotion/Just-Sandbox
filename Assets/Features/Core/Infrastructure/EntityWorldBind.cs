using Shared.Domain;
using UnityEngine;

namespace Feature.Core.Infrastructure
{
    /// <summary>
    /// Provides a component that binds an entity to a target, enabling interaction between them within the game world.
    /// </summary>
    /// <remarks>Use this class to associate an object implementing the IEntity interface with one
    /// implementing the ITarget interface. This separation ensures type safety and clarity, even if both interfaces are
    /// implemented by the same object. The Bind method establishes the relationship, and the AsEntity and AsTarget
    /// properties expose the bound interfaces for further interaction.</remarks>
    public class EntityWorldBind : MonoBehaviour
    {
        public IEntity AsEntity { get; private set; }
        public ITarget AsTarget { get; private set; }

        public void Bind(IEntity entity, ITarget target)
        {
            AsEntity = entity;
            AsTarget = target;
        }
    }
}