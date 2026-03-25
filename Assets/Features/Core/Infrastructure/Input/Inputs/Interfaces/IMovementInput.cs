using Shared.Data;
using System;

namespace Core.PlayerInput
{
    public interface IMovementInput
    {
        event Action<Position2> MoveChanged;

        event Action JumpPressed;
        event Action JumpReleased;
        event Action SprintPressed;
        event Action SprintReleased;
        void Tick();
    }
}