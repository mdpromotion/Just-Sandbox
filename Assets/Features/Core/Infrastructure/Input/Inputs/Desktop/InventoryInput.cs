using System;
using UnityEngine;

namespace Core.PlayerInput
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
            for (int i = 1; i < _keys.Length; i++)
            {
                if (Input.GetKeyDown(_keys[i]))
                {
                    SlotPressed?.Invoke(i);
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractPressed?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadPressed?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DropPressed?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                ToolPressed?.Invoke();
            }
        }
    }
}