using System;

namespace Feature.PlayerExitMenu.Presentation
{
    public interface IView
    {
        event Action<NavigationButtonType> OnNavigationButtonClicked;
        void ToggleMenu(bool state);
        void SetMenuPanelSize(float size);
    }
}