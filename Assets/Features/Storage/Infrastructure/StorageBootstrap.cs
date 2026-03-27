using System;
using Zenject;

namespace Feature.Storage.Infrastructure
{
    public class StorageBootstrap : IInitializable, IDisposable
    {
        private readonly IStorageDataService _storageData;

        public void Initialize()
        {
            _storageData.Load();
        }
        public void Dispose() 
        {
            _storageData.Save(isCloudSave: true);
        }
    }
}