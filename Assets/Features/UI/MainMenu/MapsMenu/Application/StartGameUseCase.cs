using Feature.MapsMenu.Data;
using Feature.Scene.Infrastructure;
using System.Threading.Tasks;

namespace Feature.MapsMenu.Application
{
    public class StartGameUseCase : IStartGameUseCase
    {
        private readonly ISceneManager _sceneManager;

        public StartGameUseCase(ISceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public async Task<Result> StartGame(SceneData data)
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