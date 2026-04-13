using Feature.UI.Maps.Presentation;
using Feature.Scene.Infrastructure;
using Feature.UI.Maps.Data;
using Cysharp.Threading.Tasks;

namespace Feature.UI.Maps.Application
{
    public class StartGameUseCase : IStartGameUseCase
    {
        private readonly ISceneManager _sceneManager;

        public StartGameUseCase(ISceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public async UniTask<Result> StartGame(SceneData data)
        {
            if (string.IsNullOrWhiteSpace(data.Path))
                return Result.Failure("Scene path is empty");

            var result = await _sceneManager.LoadSceneAsync(data.Path);

            if (!result.IsSuccess)
                return Result.Failure(result.Error);

            return Result.Success();
        }
    }
}