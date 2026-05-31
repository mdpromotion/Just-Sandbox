using System;
using Core.PlayerInput;
using Features.Core.Infrastructure.Input.Inputs.Interfaces;
using UnityEngine;

namespace Features.Core.Infrastructure.Input.Inputs.Desktop
{
    public class DesktopSpecialButtonInput : ISpecialButtonInput
    {
        public event Action ExitMenuPressed;

        public void Tick()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.X))
            {
                ExitMenuPressed?.Invoke();
            }
        }
    }
}