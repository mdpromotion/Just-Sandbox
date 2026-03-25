public interface IPlayerStorage
{
    int GetInt(string key);
    bool GetBool(string key);
    void Set(string key, string value);
    void Set(string key, int value);
    void Set(string key, bool value);
    void Set(string key, float value);
    void Sync(bool cloudSave);
    bool Has(string key);
    void EnableAutoSync(int interval, bool cloudSave);
}