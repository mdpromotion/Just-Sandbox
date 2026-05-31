using System;
using Core.PlayerInput;
using Features.Core.Infrastructure.Input.Inputs.Interfaces;
using UnityEngine;

namespace Features.Core.Infrastructure.Input.Inputs.Desktop
{
    public class DesktopInventoryInput : IInventoryInput
    {
        public event Action<int?> SlotPressed;
        public event Action InteractPressed;
        public event Action ReloadPressed;
        public event Action DropPressed;
        public event Action ToolPressed;

        private readonly KeyCode[] _keys;

        public DesktopInventoryInput(int slotCount = 8)
        {
            _keys = new KeyCode[slotCount];
            for (int i = 0; i < slotCount; i++)
            {
                _keys[i] = (KeyCode)((int)KeyCode.Alpha0 + i);
            }
        }

        public void Tick()
        {
            for (var i = 1; i < _keys.Length; i++)
            {
                if (UnityEngine.Input.GetKeyDown(_keys[i]))
                {
                    SlotPressed?.Invoke(i);
                }
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                InteractPressed?.Invoke();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                ReloadPressed?.Invoke();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            {
                DropPressed?.Invoke();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.H))
            {
                ToolPressed?.Invoke();
            }
        }
    }
}