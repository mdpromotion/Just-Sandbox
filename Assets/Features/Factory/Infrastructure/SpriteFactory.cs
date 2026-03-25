using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Feature.Factory.Infrastructure
{
    public class SpriteFactory : ISpriteFactory, IDisposable
    {
        public readonly static string LogTag = nameof(SpriteFactory);
        private readonly ILogger _logger;

        private readonly Dictionary<string, Sprite> _spriteCache = new();
        private readonly Dictionary<string, AsyncOperationHandle<Sprite>> _handles = new();
        private readonly Dictionary<string, Task<Sprite>> _loadingTasks = new();

        public SpriteFactory(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<Sprite> GetSprite(string address, string spriteName = null)
        {
            string key = string.IsNullOrEmpty(spriteName) ? address : $"{address}[{spriteName}]";

            if (_spriteCache.TryGetValue(key, out var cachedSprite))
                return cachedSprite;

            var loadTask = LoadSprite(key);
            _loadingTasks[key] = loadTask;

            var sprite = await loadTask;
            _loadingTasks.Remove(key);

            return sprite;
        }

        private async Task<Sprite> LoadSprite(string key)
        {
            var handle = Addressables.LoadAssetAsync<Sprite>(key);
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                _logger.LogError(LogTag, $"Failed to load sprite: {key}");
                return null;
            }

            _spriteCache[key] = handle.Result;
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
            _spriteCache.Clear();
        }
    }
}