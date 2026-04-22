using System;

namespace Feature.ExitMenu.Presentation
{
    public interface IView
    {
        event Action<NavigationButtonType> OnNavigationButtonClicked;
        void ToggleMenu(bool state);
        void SetMenuPanelSize(float size);
    }
}