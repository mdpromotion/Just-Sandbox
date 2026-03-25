using Shared.Data;

namespace Feature.Player.Application
{
    public interface IPhysicsController
    {
        Position3 CurrentVelocity { get; }
        Result Move(Position3 velocity);
        Result Punch(Position3 impulse, float modifier = 1f);
        bool IsGrounded();
        void SwitchKinematicState(bool isKinematic);
        void SwitchRigidbodyGravity(bool useGravity);
        void ToggleConstraints(bool state);
        void ResetVelocity();
    }
}