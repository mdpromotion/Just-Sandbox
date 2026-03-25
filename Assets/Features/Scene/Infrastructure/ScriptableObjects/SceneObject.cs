using UnityEngine;

namespace Feature.Scene.Infrastructure
{
    [CreateAssetMenu(fileName = "SceneObject", menuName = "SceneObject", order = 1)]
    public class SceneObject : ScriptableObject
    {
        public string SceneName;
        public string ScenePath;
    }
}