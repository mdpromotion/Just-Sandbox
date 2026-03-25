using System;
using UnityEngine;

namespace Core.PlayerInput
{
    public interface IInteractionInput
    {
        event Action InteractPressed;
        event Action DropPressed;
        event Action ReloadPressed;
        event Action<MouseButton> MouseDown;
        event Action<MouseButton> MouseUp;
        event Action<MouseButton> MouseHeld;
        void Tick();
    }
}