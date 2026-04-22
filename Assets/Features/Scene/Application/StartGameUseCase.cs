using Cysharp.Threading.Tasks;
using Feature.Scene.Infrastructure;

namespace Feature.Scene.Application
{
    public class LoadSceneUseCase : ILoadSceneUseCase
    {
        private readonly ISceneManager _sceneManager;

        public LoadSceneUseCase(ISceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public async UniTask LoadScene(string pathData)
        {
            if (!string.IsNullOrEmpty(pathData))
            {
                await _sceneManager.LoadSceneAsync(pathData);
            }
        }
    }
}