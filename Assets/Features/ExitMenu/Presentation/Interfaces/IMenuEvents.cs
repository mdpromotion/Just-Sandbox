using System;

namespace Feature.ExitMenu.Domain
{
    public interface IMenuEvents
    {
        event Action<bool> MenuToggled;
    }
}