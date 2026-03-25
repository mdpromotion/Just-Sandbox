using UnityEngine.ResourceManagement.ResourceProviders;

namespace Feature.Scene.Infrastructure
{
    public readonly struct LoadedSceneData
    {
        public readonly SceneInstance SceneInstance;
        public readonly UnityEngine.SceneManagement.Scene Scene;
        public LoadedSceneData(SceneInstance sceneInstance)
        {
            SceneInstance = sceneInstance;
            Scene = sceneInstance.Scene;
        }
    }
}