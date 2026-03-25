using Shared.Data;

namespace Feature.Player.Application
{
    public enum VerticalMove
    {
        Down = -1,
        None = 0,
        Up = 1
    }

    public class MovementInputState : IReadOnlyMovementInputState
    {
        public Position2 InputDirection { get; set; } = Position2.Zero;
        public VerticalMove VerticalInput { get; set; }
        public bool IsJumping { get; set; } = false;
        public bool IsSprinting { get; set; } = false;
    }
}