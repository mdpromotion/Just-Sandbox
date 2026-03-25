using Feature.Toolbox.Application;
using System;
using Zenject;

namespace Feature.Toolbox.Presentation
{
    public class SpawnSettingsPresenter : IInitializable, IDisposable
    {
        private readonly IMenuUseCase _menuInput;
        private readonly IView _view;

        private int currentTextureIndex = 0;

        public SpawnSettingsPresenter(IView view, IMenuUseCase menuInput)
        {
            _view = view;
            _menuInput = menuInput;
        }

        public void Initialize()
        {
            _view.SettingsButtonClicked += OnSettingsButtonClicked;
        }

        private void OnSettingsButtonClicked(SpawnSettingsButtonData data)
        {
            switch (data.Type)
            {
                case SpawnSettings.InventorySpawn:
                    bool enabled = _menuInput.ToggleInventorySpawn();
                    _view.ToggleInventorySpawnButton(enabled);
                    break;

                case SpawnSettings.SelectTexture:
                    int textureId = data.Value;
                    _menuInput.SelectTexture(textureId);
                    if (currentTextureIndex != -1)
                        _view.ToggleTextureSpawnButton(currentTextureIndex, false);

                    _view.ToggleTextureSpawnButton(textureId, true);
                    currentTextureIndex = textureId;
                    break;
            }
        }

        public void Dispose()
        {
            _view.SettingsButtonClicked -= OnSettingsButtonClicked;
        }
    }
}