using System;
using Core.PlayerInput;
using Features.Core.Infrastructure.Input.Inputs.Interfaces;
using Shared.Data;
using UnityEngine;

namespace Features.Core.Infrastructure.Input.Inputs.Desktop
{
    public class DesktopMovementInput : IMovementInput
    {
        public event Action JumpPressed;
        public event Action JumpReleased;
        public event Action SprintPressed;
        public event Action SprintReleased;

        public event Action<Position2> MoveChanged;

        public void Tick()
        {
            bool jumpPressed = UnityEngine.Input.GetKeyDown(KeyCode.Space);
            bool jumpReleased = UnityEngine.Input.GetKeyUp(KeyCode.Space);
            bool sprintPressed = UnityEngine.Input.GetKeyDown(KeyCode.LeftShift) || UnityEngine.Input.GetKeyDown(KeyCode.RightShift);
            bool sprintReleased = UnityEngine.Input.GetKeyUp(KeyCode.LeftShift) || UnityEngine.Input.GetKeyUp(KeyCode.RightShift);

            if (jumpPressed)
            {
                JumpPressed?.Invoke();
            }
            if (jumpReleased)
            {
                JumpReleased?.Invoke();
            }
            if (sprintPressed)
            {
                SprintPressed?.Invoke();
            }
            if (sprintReleased)
            {
                SprintReleased?.Invoke();
            }
            Position2 moveInput = new Position2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
            MoveChanged?.Invoke(moveInput);
        }
    }
}