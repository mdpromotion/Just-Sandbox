using System;

namespace Features.Core.Infrastructure.Input.Inputs.Interfaces
{
    public interface ISpecialButtonInput
    {
        event Action ExitMenuPressed;
        void Tick();
    }
}