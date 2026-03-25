using Feature.Player.Application;
using UnityEngine;
using Zenject;

namespace Feature.Player.Infrastructure
{
    public class GroundCheckUpdater : IFixedTickable
    {
        private readonly IPhysicsController _physics;
        private readonly PlayerWorldState _state;
        private float _timer;

        public GroundCheckUpdater(IPhysicsController physics, PlayerWorldState state)
        {
            _physics = physics;
            _state = state;
        }

        public void FixedTick()
        {
            _timer += Time.fixedDeltaTime;
            if (_timer < 0.05f) return;

            _timer = 0f;
            _state.IsGrounded = _physics.IsGrounded();
        }
    }
}