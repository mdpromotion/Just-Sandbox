using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Feature.Scene.Infrastructure
{
    public class SceneManager : ISceneManager
    {
        private readonly List<LoadedSceneData> _loadedScenes = new();
        private readonly SceneLoadService _sceneLoadService;

        public SceneManager(SceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }

        public async UniTask<Result> LoadSceneAsync(string path)
        {
            var loadResult = await _sceneLoadService.LoadScene(path);
            if (!loadResult.IsSuccess)
                return Result.Failure(loadResult.Error);

            var unloadResult = await UnloadAllAsync();
            if (!unloadResult.IsSuccess)
                return unloadResult;

            _loadedScenes.Add(loadResult.Value);

            return Result.Success();
        }

        private async UniTask<Result> UnloadAllAsync()
        {
            if (_loadedScenes.Count == 0)
                return Result.Success();

            var scenesToUnload = new List<LoadedSceneData>(_loadedScenes);

            foreach (var sceneData in scenesToUnload)
            {
                var result = await _sceneLoadService.UnloadSceneAsync(sceneData.SceneInstance);
                if (!result.IsSuccess)
                    return result;
            }

            _loadedScenes.Clear();

            return Result.Success();
        }
    }
}