using Shared.Data;
using Shared.Providers;
using UnityEngine;
using Zenject;

namespace Feature.Player.Infrastructure
{
    public class TransformProvider : IPlayerTransformController, IPlayerTransformData
    {
        private readonly Transform _transform;
        public Position3 Position => Mapper.ToPosition3(_transform.position);
        public Position3 Forward => Mapper.ToPosition3(_transform.forward);
        public Position3 Right => Mapper.ToPosition3(_transform.right);

        public TransformProvider(
            [Inject(Id = "Player")] Transform transform)
        {
            _transform = transform;
        }

        public void Teleport(Position3 position)
        {
            _transform.position = Mapper.ToVector3(position);
        }

        public void ResetAngle()
        {
            _transform.rotation = Quaternion.identity;
        }

        public void ApplyYaw(float yaw)
        {
            _transform.rotation = Quaternion.Euler(0, yaw, 0);
        }
    }
}