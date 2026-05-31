using System;
using Core.PlayerInput;
using Features.Core.Infrastructure.Input.Inputs.Interfaces;
using UnityEngine;

namespace Features.Core.Infrastructure.Input.Inputs.Desktop
{
    public enum MouseButton
    {
        Left,
        Right
    }

    public class DesktopInteractionInput : IInteractionInput
    {
        public event Action InteractPressed;
        public event Action DropPressed;
        public event Action ReloadPressed;

        public event Action<MouseButton> MouseDown;
        public event Action<MouseButton> MouseUp;
        public event Action<MouseButton> MouseHeld;

        private static readonly MouseButton[] AllButtons =
        {
            MouseButton.Left,
            MouseButton.Right
        };

        public void Tick()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                InteractPressed?.Invoke();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            {
                DropPressed?.Invoke();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                ReloadPressed?.Invoke();
            }
            foreach (MouseButton button in AllButtons)
            {
                int index = (int)button;
                if (UnityEngine.Input.GetMouseButton(index)) MouseHeld?.Invoke(button);
                if (UnityEngine.Input.GetMouseButtonUp(index)) MouseUp?.Invoke(button);
                if (UnityEngine.Input.GetMouseButtonDown(index)) MouseDown?.Invoke(button);
            }
        }
    }
}