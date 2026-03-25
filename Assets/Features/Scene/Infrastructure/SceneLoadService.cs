using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Feature.Scene.Infrastructure
{
    public class SceneLoadService
    {
        public async Task<Result<LoadedSceneData>> LoadScene(
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

        public async Task<Result> UnloadSceneAsync(SceneInstance scene)
        {
            var handle = Addressables.UnloadSceneAsync(scene);

            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
                return Result.Failure($"Failed to unload scene: {scene.Scene.name}");

            return Result.Success();
        }
    }
}