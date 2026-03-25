using Feature.Scene.Infrastructure;
using System;

namespace Feature.MainMenu.Presentation
{
    public interface IView
    {
        event Action<NavigationButtonType> OnNavigationButtonClicked;
        event Action<SceneObject> OnSceneButtonClicked;
        void SetButtonsPanelSize(float size);
        void SetMapsPanelSize(float size);
    }
}