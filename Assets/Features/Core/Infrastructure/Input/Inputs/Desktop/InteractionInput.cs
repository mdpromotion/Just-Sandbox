using System;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.PlayerInput
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

        private static readonly MouseButton[] _allButtons =
        {
            MouseButton.Left,
            MouseButton.Right
        };

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractPressed?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DropPressed?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadPressed?.Invoke();
            }
            foreach (MouseButton button in _allButtons)
            {
                int index = (int)button;
                if (Input.GetMouseButton(index)) MouseHeld?.Invoke(button);
                if (Input.GetMouseButtonUp(index)) MouseUp?.Invoke(button);
                if (Input.GetMouseButtonDown(index)) MouseDown?.Invoke(button);
            }
        }
    }
}