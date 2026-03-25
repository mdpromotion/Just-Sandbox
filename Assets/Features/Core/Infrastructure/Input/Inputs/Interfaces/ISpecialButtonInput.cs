using System;

namespace Core.PlayerInput
{
    public interface ISpecialButtonInput
    {
        event Action ExitMenuPressed;
        void Tick();
    }
}