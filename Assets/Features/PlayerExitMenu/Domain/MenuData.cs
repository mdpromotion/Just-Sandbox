using System;

namespace Feature.PlayerExitMenu.Domain
{
    public class MenuData : IMenuData, IMenuEvents
    {
        public bool IsMenuActive { get; private set; }
        public event Action<bool> MenuToggled;

        public bool ToggleMenu()
        {
            IsMenuActive = !IsMenuActive;
            MenuToggled?.Invoke(IsMenuActive);
            return IsMenuActive;
        }
    }
}