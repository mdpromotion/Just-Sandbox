using Feature.PlayerExitMenu.Application;
using Feature.PlayerExitMenu.Domain;
using Feature.UI.Presentation;
using System;
using Zenject;

namespace Feature.PlayerExitMenu.Presentation
{
    public class Presenter : IInitializable, IDisposable
    {
        private readonly IMenuUseCase _menuUseCase;
        private readonly IMenuEvents _events;
        private readonly IAnimator _animator;
        private readonly IView _view;

        public Presenter(IMenuUseCase menuUseCase, IMenuEvents events, IAnimator animator, IView view)
        {
            _menuUseCase = menuUseCase;
            _events = events;
            _animator = animator;
            _view = view;
        }

        public void Initialize()
        {
            _events.MenuToggled += OnMenuToggled;
            _view.OnNavigationButtonClicked += OnNavigationButtonClicked;
        }
        
        private async void OnNavigationButtonClicked(NavigationButtonType type)
        {
            switch (type)
            {
                case NavigationButtonType.Continue:
                    await _menuUseCase.LoadScene("Menu");
                    break;
                case NavigationButtonType.Exit:
                    _menuUseCase.ToggleMenu();
                    break;
            }
        }

        private void OnMenuToggled(bool state)
        {
            float from = state ? 0f : 1f;
            float to = state ? 1f : 0f;
            float duration = 0.5f;

            _view.SetMenuPanelSize(from);

            _animator.Animate(from, to, _view.SetMenuPanelSize, duration);

        }

        public void Dispose() 
        {
            _events.MenuToggled -= OnMenuToggled;
            _view.OnNavigationButtonClicked -= OnNavigationButtonClicked;
        }
    }
}