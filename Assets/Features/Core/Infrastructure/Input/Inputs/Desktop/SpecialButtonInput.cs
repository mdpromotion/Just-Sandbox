using System;
using UnityEngine;

namespace Core.PlayerInput
{
    public class DesktopSpecialButtonInput : ISpecialButtonInput
    {
        public event Action ExitMenuPressed;

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ExitMenuPressed?.Invoke();
            }
        }
    }
}