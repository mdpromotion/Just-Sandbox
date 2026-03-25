using Feature.MainMenu.Application;
using Feature.MainMenu.Data;
using Feature.Scene.Infrastructure;
using Feature.UI.Presentation;
using System;
using System.Threading.Tasks;
using Zenject;

namespace Feature.MainMenu.Presentation
{
    public class Presenter : IInitializable, IDisposable
    {
        private readonly IView _view;
        private readonly IAnimator _animator;
        private readonly IStartGameUseCase _startGameUseCase;

        private bool _isMapsMenuActive = false;

        public Presenter(IView view, IAnimator animator, IStartGameUseCase startGameUseCase)
        {
            _view = view;
            _animator = animator;
            _startGameUseCase = startGameUseCase;
        }

        public void Initialize() 
        {
            _view.OnNavigationButtonClicked += OnNavigationButtonClicked;
            _view.OnSceneButtonClicked += OnSceneButtonClicked;
        }

        public void OnNavigationButtonClicked(NavigationButtonType type)
        {
            switch (type) 
            {
                case NavigationButtonType.OpenMaps:
                    _isMapsMenuActive = true;
                    ToggleButtonsPanel(!_isMapsMenuActive);
                    ToggleMapsPanel(_isMapsMenuActive);
                    break;
                case NavigationButtonType.CloseMaps:
                    _isMapsMenuActive = false;
                    ToggleButtonsPanel(!_isMapsMenuActive);
                    ToggleMapsPanel(_isMapsMenuActive);
                    break;
            }
        }

        public void OnSceneButtonClicked(SceneObject scene)
        {
            _ = HandleSceneButtonClickedAsync(scene);
        }

        private async Task HandleSceneButtonClickedAsync(SceneObject scene)
        {
            var sceneData = new SceneData(scene.SceneName, scene.ScenePath);

            await _startGameUseCase.StartGame(sceneData);
        }

        private void ToggleButtonsPanel(bool state)
        {
            float from = state ? 0f : 1f;
            float to = state ? 1f : 0f;
            float duration = 0.5f;

            _view.SetButtonsPanelSize(from);

            _animator.Animate(from, to, value => _view.SetButtonsPanelSize(value), duration);
        }

        private void ToggleMapsPanel(bool state)
        {
            float from = state ? 0f : 1f;
            float to = state ? 1f : 0f;
            float duration = 0.5f;

            _view.SetButtonsPanelSize(from);

            _animator.Animate(from, to, value => _view.SetMapsPanelSize(value), duration);
        }

        public void Dispose()
        {
            _view.OnNavigationButtonClicked -= OnNavigationButtonClicked;
        }
    }
}