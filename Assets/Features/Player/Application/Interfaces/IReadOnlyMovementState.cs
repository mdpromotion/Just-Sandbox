using Shared.Data;

namespace Feature.Player.Application
{
    public interface IReadOnlyMovementInputState
    {
        Position2 InputDirection { get; }
        VerticalMove VerticalInput { get; }
        bool IsJumping { get; }
        bool IsSprinting { get; }
    }
}