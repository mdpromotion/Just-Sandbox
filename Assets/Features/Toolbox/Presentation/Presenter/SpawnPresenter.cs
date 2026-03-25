using Feature.Toolbox.Application;
using System;
using System.Collections.Generic;
using Zenject;

namespace Feature.Toolbox.Presentation
{
    public class SpawnPresenter : IInitializable, IDisposable
    {
        private readonly Dictionary<SpawnCategory, ISpawnUseCase> _spawn;
        private readonly IView _view;

        public SpawnPresenter(IView view, Dictionary<SpawnCategory, ISpawnUseCase> spawn)
        {
            _view = view;
            _spawn = spawn;
        }

        public void Initialize()
        {
            _view.SpawnButtonClicked += OnSpawnButtonClicked;
        }

        public async void OnSpawnButtonClicked(SpawnButtonData data)
        {
            if (!_spawn.TryGetValue(data.Category, out var spawnUseCase))
                return;

            await spawnUseCase.TrySpawn(data.Id);
        }

        public void Dispose()
        {
            _view.SpawnButtonClicked -= OnSpawnButtonClicked;
        }
    }
}