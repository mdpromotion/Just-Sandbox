using Feature.Toolbox.Presentation.Buttons;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Toolbox.Presentation
{
    public class View : MonoBehaviour, IView
    {
        [Header("Buttons")]
        private List<SpawnButtonView> _spawnButtons;
        private List<TabButtonView> _tabButtons;
        private List<SpawnModeButtonView> _spawnModeButtons;
        private List<TextureButtonView> _textureButtons;

        [Header("Menu")]
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject[] _tabs;

        [Header("Colors")]
        [SerializeField] private Color _tabActiveColor;
        [SerializeField] private Color _tabDisableColor;
        [SerializeField] private Color _buttonActiveColor;
        [SerializeField] private Color _buttonDisableColor;

        #region Events

        public event Action<SpawnButtonData> SpawnButtonClicked;
        public event Action<int> TabButtonClicked;
        public event Action<SpawnSettingsButtonData> SettingsButtonClicked;

        #endregion

        #region Construct

        [Inject]
        public void Construct(
            List<SpawnButtonView> buttons,
            List<TabButtonView> tabButtons,
            List<SpawnModeButtonView> spawnModeButtons,
            List<TextureButtonView> textureButtons)
        {
            _spawnButtons = buttons;
            _tabButtons = tabButtons;
            _spawnModeButtons = spawnModeButtons;
            _textureButtons = textureButtons;

            Subscribe();
        }

        #endregion

        #region Subscriptions

        private void Subscribe()
        {
            foreach (var button in _spawnButtons)
                button.Clicked += OnSpawnButtonClicked;

            foreach (var button in _tabButtons)
                button.Clicked += OnTabButtonClicked;

            foreach (var button in _spawnModeButtons)
                button.Clicked += OnSpawnModeButtonClicked;

            foreach (var button in _textureButtons)
                button.Clicked += OnTextureButtonClicked;

        }

        private void Unsubscribe()
        {
            foreach (var button in _spawnButtons)
                button.Clicked -= OnSpawnButtonClicked;

            foreach (var button in _tabButtons)
                button.Clicked -= OnTabButtonClicked;

            foreach (var button in _spawnModeButtons)
                button.Clicked -= OnSpawnModeButtonClicked;

            foreach (var button in _textureButtons)
                button.Clicked -= OnTextureButtonClicked;
        }

        #endregion

        #region UI API

        public void ToggleToolbox(bool state)
            => _panel.SetActive(state);

        public void ToggleTab(int index, bool active)
            => _tabs[index].SetActive(active);

        public void ToggleButton(int index, bool active)
            => _tabButtons[index].ChangeColor(active ? _tabActiveColor : _tabDisableColor);

        public void ToggleInventorySpawnButton(bool active)
        {
            foreach (var button in _spawnModeButtons)
                button.ChangeColor(active ? _buttonActiveColor : _buttonDisableColor);
        }

        public void ToggleTextureSpawnButton(int index, bool active)
            => _textureButtons[index].ToggleMark(active);

        #endregion

        #region UI Event Handlers

        private void OnSpawnButtonClicked(SpawnButtonData data)
            => SpawnButtonClicked?.Invoke(data);

        private void OnTabButtonClicked(int id)
            => TabButtonClicked?.Invoke(id);

        private void OnSpawnModeButtonClicked()
            => SettingsButtonClicked?.Invoke(new SpawnSettingsButtonData(SpawnSettings.InventorySpawn, -1));

        private void OnTextureButtonClicked(int index)
            => SettingsButtonClicked?.Invoke(new SpawnSettingsButtonData(SpawnSettings.SelectTexture, index));

        #endregion

        #region Unity Lifecycle

        private void OnDestroy()
        {
            Unsubscribe();
        }

        #endregion
    }
}