using Shared.Data;
using System;
using UnityEngine;

namespace Core.PlayerInput
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
            bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
            bool jumpReleased = Input.GetKeyUp(KeyCode.Space);
            bool sprintPressed = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
            bool sprintReleased = Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift);

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
            Position2 moveInput = new Position2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            MoveChanged?.Invoke(moveInput);
        }
    }
}