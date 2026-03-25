public interface IStorageDataService
{
    void ModifyMoney(int amount);
    void SetMoney(int amount);
    void SetPurchase(PurchaseItemData item, bool value);
    void CompleteTutorial();
    void SetMusic(bool value);
    void SetGraphicsQuality(int quality);
    void SetMouseSensitivity(int value);

    void Load();
    void Save(bool isCloudSave);
}
