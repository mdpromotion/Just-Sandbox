using System;
using UnityEngine;

namespace Core.PlayerInput
{
    public interface IInventoryInput
    {
        event Action<int?> SlotPressed;
        event Action ReloadPressed;
        event Action ToolPressed;
        void Tick();
    }
}