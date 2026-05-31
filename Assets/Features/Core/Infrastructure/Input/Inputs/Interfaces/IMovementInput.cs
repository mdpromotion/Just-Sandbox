using System;
using Shared.Data;

namespace Features.Core.Infrastructure.Input.Inputs.Interfaces
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