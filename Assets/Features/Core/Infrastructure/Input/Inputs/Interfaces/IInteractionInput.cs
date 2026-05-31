using System;
using Features.Core.Infrastructure.Input.Inputs.Desktop;

namespace Features.Core.Infrastructure.Input.Inputs.Interfaces
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