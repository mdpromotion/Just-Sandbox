using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Feature.Scene.Infrastructure
{
    public class SceneLoadService
    {
        public async UniTask<Result<LoadedSceneData>> LoadScene(
            string scenePath,
            LoadSceneMode mode = LoadSceneMode.Single,
            bool releaseOnLoad = false)
        {
            var handle = Addressables.LoadSceneAsync(
                scenePath,
                mode,
                activateOnLoad: true
            );

            if (releaseOnLoad)
                handle.ReleaseHandleOnCompletion();

            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
                return Result<LoadedSceneData>.Failure($"Failed to load scene at path: {scenePath}");

            return Result<LoadedSceneData>.Success(new LoadedSceneData(handle.Result));
        }
    }
}