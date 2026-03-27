using Cysharp.Threading.Tasks;

namespace Feature.Scene.Infrastructure
{
    public class SceneManager : ISceneManager
    {
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

            return Result.Success();
        }
    }
}