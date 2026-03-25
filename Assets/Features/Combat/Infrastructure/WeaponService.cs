#nullable enable
using Core.Service;
using Feature.Core.Infrastructure;
using Shared.Domain;
using Shared.Providers;
using UnityEngine;

namespace Feature.Combat.Infrastructure
{
    public class WeaponService : IWeaponService
    {
        private readonly IRaycastService _raycast;
        private readonly ICameraTransformData _transform;

        private readonly LayerMask _layerMask = LayerMask.GetMask("NPC");

        public WeaponService(IRaycastService raycast, ICameraTransformData transform)
        {
            _raycast = raycast;
            _transform = transform;
        }

        public ITarget? GetTarget(float distance)
        {
            Vector3 origin = _transform.Position;
            Vector3 direction = _transform.Forward;

            var targetObject = _raycast.GetRaycastObject(origin, direction, distance, _layerMask);
            if (targetObject != null && targetObject.TryGetComponent<EntityWorldBind>(out var entity))
            {
                return entity.AsTarget;
            }
            return null;
        }

    }
}