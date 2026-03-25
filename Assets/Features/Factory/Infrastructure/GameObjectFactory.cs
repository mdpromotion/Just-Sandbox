using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Feature.Factory.Infrastructure
{
    public class GameObjectFactory : IGameObjectFactory
    {
        public async Task<GameObject> SpawnObject(
            string prefabAddress,
            Vector3 position,
            Quaternion rotation,
            Material material = null)
        {
            var handle = Addressables.InstantiateAsync(prefabAddress, position, rotation);
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Failed to spawn prefab: {prefabAddress}");
                return null;
            }

            var obj = handle.Result;
            obj.name = RemoveClone(obj.name);

            return obj;
        }

        public void Release(GameObject obj)
        {
            Addressables.ReleaseInstance(obj);
        }

        private string RemoveClone(string name) => name.Replace("(Clone)", "");
    }
}