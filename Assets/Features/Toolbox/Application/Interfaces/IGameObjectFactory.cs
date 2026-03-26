using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Feature.Factory.Infrastructure
{
    public interface IGameObjectFactory
    {
        UniTask<GameObject> SpawnObject(string prefabAddress, Vector3 position, Quaternion rotation, Material material = null);
        void Release(GameObject obj);
    }
}