using GamePush;

namespace Feature.Storage.Infrastructure
{
    public class PlayerStorage : IPlayerStorage
    {
        public int GetInt(string key)
        {
            return GP_Player.GetInt(key);
        }
        public bool GetBool(string key)
        {
            return GP_Player.GetBool(key);
        }
        public void Set(string key, string value)
        {
            GP_Player.Set(key, value);
        }
        public void Set(string key, int value)
        {
            GP_Player.Set(key, value);
        }
        public void Set(string key, bool value)
        {
            GP_Player.Set(key, value);
        }
        public void Set(string key, float value)
        {
            GP_Player.Set(key, value);
        }
        public bool Has(string key)
        {
            return GP_Player.Has(key);
        }
        public void Sync(bool cloudSave)
        {
            GP_Player.Sync(
                storage: cloudSave ? SyncStorageType.cloud : SyncStorageType.local
            );
        }
        public void EnableAutoSync(int interval, bool cloudSave)
        {
            GP_Player.EnableAutoSync(
                interval: interval,
                storage: cloudSave ? SyncStorageType.preferred : SyncStorageType.local
            );
        }
    }
}