using Feature.Storage.Domain;
using System;
using System.Collections.Generic;

namespace Feature.Storage.Infrastructure
{
    public class StorageDataService : IStorageDataService
    {
        private readonly IPlayerStorage _storage;

        private readonly PlayerEconomy _playerEconomy;
        private readonly PurchaseItem _purchaseItem;
        private readonly ControlSettings _controlSettings;
        private readonly GraphicsSettings _graphicsSettings;
        private readonly AudioSettings _audioSettings;
        private readonly PlayerProgress _playerProgress;

        public StorageDataService(IPlayerStorage storage, PlayerEconomy playerEconomy,
            ControlSettings controlSettings, GraphicsSettings graphicsSettings, AudioSettings audioSettings,
            PlayerProgress playerProgress)
        {
            _storage = storage;
            _playerEconomy = playerEconomy;
            _controlSettings = controlSettings;
            _graphicsSettings = graphicsSettings;
            _audioSettings = audioSettings;
            _playerProgress = playerProgress;
        }

        private readonly Dictionary<PurchaseItemData, string> _purchaseKeys = new()
        {
            { PurchaseItemData.Rifle, "isRiflePurchased" },
            { PurchaseItemData.M4, "isM4Purchased" },
            { PurchaseItemData.Turret, "isTurretPurchased" },
            { PurchaseItemData.Set, "isSetPurchased" },
            { PurchaseItemData.CheatMenu, "isCheatMenuPurchased" }
        };

        /// <summary>
        /// Loads user settings and game progress from persistent storage and applies them to the current session.
        /// </summary>
        /// <remarks>This method retrieves saved values such as currency, mouse sensitivity, graphics quality,
        /// tutorial completion status, and purchased items, and updates the current state accordingly. It also enables
        /// automatic synchronization of storage with cloud saving enabled. Call this method during application startup or
        /// when reloading user data to ensure the session reflects the latest saved state.</remarks>
        public void Load()
        {
            SetMoney(_storage.GetInt("money"));
            SetMouseSensitivity(_storage.GetInt("mouseSensitivity"));
            SetGraphicsQuality(_storage.GetInt("graphicsQuality"));
            SetMusic(_storage.GetBool("isMusicOn"));

            if (_storage.GetBool("isTutorialCompleted"))
                CompleteTutorial();

            foreach (var kvp in _purchaseKeys)
            {
                var item = kvp.Key;
                var key = kvp.Value;
                if (_storage.Has(key))
                {
                    bool purchased = _storage.GetBool(key);
                    _purchaseItem.SetPurchase(item, purchased);
                }
            }

            _storage.EnableAutoSync(interval: 45, cloudSave: true);
        }

        /// <summary>
        /// Saves the current game settings and progress to persistent storage, with an option to synchronize data to the
        /// cloud.
        /// </summary>
        /// <remarks>This method persists key player settings and purchase states. If cloud synchronization is
        /// enabled, the saved data will be uploaded to the cloud, allowing access from other devices. Otherwise, data is
        /// saved only on the local device.</remarks>
        /// <param name="cloudSave">true to synchronize the saved data with cloud storage; false to save data locally only.</param>
        public void Save(bool cloudSave)
        {
            _storage.Set("money", _playerEconomy.Money);
            _storage.Set("mouseSensitivity", _controlSettings.MouseSensitivity);
            _storage.Set("graphicsQuality", (int)_graphicsSettings.GraphicsQuality);
            _storage.Set("isMusicOn", _audioSettings.IsMusicEnabled);
            _storage.Set("isTutorialCompleted", _playerProgress.IsTutorialCompleted);
            foreach (var kvp in _purchaseKeys)
            {
                var item = kvp.Key;
                var key = kvp.Value;
                _storage.Set(key, _purchaseItem.IsPurchased(item));
            }

            _storage.Sync(cloudSave);
        }

        private void SetMoney(int amount)
        {
            _playerEconomy.SetMoney(amount);
        }

        private void CompleteTutorial()
        {
            _playerProgress.CompleteTutorial();
        }

        private void SetMusic(bool value)
        {
            _audioSettings.ToggleMusic(value);
        }

        private void SetGraphicsQuality(int quality)
        {
            GraphicsQuality graphicsQuality = ConvertToGraphics(quality);
            _graphicsSettings.SetGraphicsQuality(graphicsQuality);
        }

        private void SetMouseSensitivity(int value)
        {
            _controlSettings.SetMouseSensitivity(value);
        }

        private GraphicsQuality ConvertToGraphics(int index)
        {
            if (Enum.TryParse<GraphicsQuality>(index.ToString(), out var quality))
            {
                return quality;
            }
            return GraphicsQuality.Low;
        }

    }
}