namespace Feature.Storage.Infrastructure
{
    public interface IStorageDataService
    {
        void Load();
        void Save(bool isCloudSave);
    }
}