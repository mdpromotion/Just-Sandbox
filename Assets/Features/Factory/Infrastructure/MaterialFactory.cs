using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Feature.Factory.Infrastructure
{
    public class MaterialFactory : IMaterialFactory, IDisposable
    {
        public readonly static string LogTag = nameof(MaterialFactory);
        private readonly ILogger _logger;

        private readonly Dictionary<string, Material> _materialCache = new();
        private readonly Dictionary<string, AsyncOperationHandle<Material>> _handles = new();
        private readonly Dictionary<string, UniTask<Material>> _loadingTasks = new();

        public MaterialFactory(ILogger logger)
        {
            _logger = logger;
        }

        public async UniTask<Material> GetMaterial(string address)
        {
            if (_materialCache.TryGetValue(address, out var cachedSprite))
                return cachedSprite;

            var loadTask = LoadMaterial(address);
            _loadingTasks[address] = loadTask;

            var sprite = await loadTask;
            _loadingTasks.Remove(address);

            return sprite;
        }

        private async UniTask<Material> LoadMaterial(string key)
        {
            var handle = Addressables.LoadAssetAsync<Material>(key);
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                _logger.LogError(LogTag, $"Failed to load sprite: {key}");
                return null;
            }

            _materialCache[key] = handle.Result;
            _handles[key] = handle;

            return handle.Result;
        }

        public void Dispose()
        {
            foreach (var handle in _handles.Values)
            {
                Addressables.Release(handle);
            }

            _handles.Clear();
            _materialCache.Clear();
        }
    }
}