using Feature.Player.Application;
using Shared.Data;
using Shared.Providers;
using UnityEngine;
using Zenject;

namespace Feature.Player.Infrastructure
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public sealed class PhysicsController : MonoBehaviour, IPhysicsController
    {
        public Position3 CurrentVelocity => Mapper.ToPosition3(_rb.linearVelocity);
        private Rigidbody _rb;
        private CapsuleCollider _collider;
        private IPlayerTransformData _playerTransformData;

        private bool _knockbackActive;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
        }

        [Inject]
        public void Construct(IPlayerTransformData data)
        {
            _playerTransformData = data;
        }

        public Result Move(Position3 velocity)
        {
            if (_rb.isKinematic)
                return Result.Failure("Rigidbody is kinematic and cannot be moved.");

            if (_knockbackActive)
                return Result.Failure("Player is currently under knockback effect and cannot be moved.");

            var vectorVelocity = Mapper.ToVector3(velocity);

            _rb.linearVelocity = vectorVelocity;
            return Result.Success();
        }
        public Result Punch(Position3 impulse, float modifier = 1f)
        {
            if (_rb.isKinematic)
                return Result.Failure("Rigidbody is kinematic and cannot be punched.");

            _knockbackActive = true;
            var vectorImpulse = Mapper.ToVector3(impulse);

            _rb.AddForce(vectorImpulse * modifier, ForceMode.VelocityChange);
            return Result.Success();
        }

        public void ToggleConstraints(bool state)
        {
            _rb.constraints = state ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.None;
        }

        public void SwitchKinematicState(bool isKinematic)
        {
            if (_rb.isKinematic == isKinematic) return;
            _rb.isKinematic = isKinematic;
        }

        public void SwitchRigidbodyGravity(bool useGravity)
        {
            if (_rb.useGravity == useGravity) return;
            _rb.useGravity = useGravity;
        }
        public void ResetVelocity()
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
        public bool IsGrounded()
        {
            float radius = _collider.radius * 1f;
            Vector3 center = Mapper.ToVector3(_playerTransformData.Position) + _collider.center;
            Vector3 checkPos = center - Vector3.up * (_collider.height / 2f - radius + 0.1f);

            bool isGrounded = Physics.CheckSphere(checkPos, radius, LayerMask.GetMask("Default", "Interactable"), QueryTriggerInteraction.Ignore);

            if (_knockbackActive && isGrounded)
                _knockbackActive = false;

            return isGrounded;
        }
    }
}