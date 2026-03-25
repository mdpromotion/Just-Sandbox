using System;

namespace Feature.PlayerExitMenu.Domain
{
    public interface IMenuEvents
    {
        event Action<bool> MenuToggled;
    }
}