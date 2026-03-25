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

        private readonly int _slotCount;

        public DesktopInventoryInput(int slotCount = 8)
        {
            _slotCount = slotCount;
        }

        public void Tick()
        {
            for (int i = 1; i < _slotCount; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
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