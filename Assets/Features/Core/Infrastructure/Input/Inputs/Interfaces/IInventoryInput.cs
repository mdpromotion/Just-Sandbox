using System;

namespace Features.Core.Infrastructure.Input.Inputs.Interfaces
{
    public interface IInventoryInput
    {
        event Action<int?> SlotPressed;
        event Action ReloadPressed;
        event Action ToolPressed;
        void Tick();
    }
}