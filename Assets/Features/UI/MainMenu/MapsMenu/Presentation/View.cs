using Feature.Scene.Infrastructure;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.MapsMenu.Presentation
{
    public class View : MonoBehaviour, IView
    {
        [SerializeField] private GameObject _buttonsPanel;
        [SerializeField] private GameObject _mapsPanel;

        private List<NavigationButton> _navigationButtons;
        private List<LoadSceneButton> _loadSceneButtons;

        public event Action<NavigationButtonType> OnNavigationButtonClicked;
        public event Action<SceneObject> OnSceneButtonClicked;

        [Inject]
        public void Construct(
            List<NavigationButton> navigationButtons,
            List<LoadSceneButton> loadSceneButtons)
        {
            _navigationButtons = navigationButtons;
            _loadSceneButtons = loadSceneButtons;
        }

        private void Start()
        {
            foreach (var button in _navigationButtons) 
                button.OnClicked += HandleNavigationButtonClicked;
            foreach (var button in _loadSceneButtons)
                button.OnClicked += HandleLoadSceneButtonClicked;
        }

        public void HandleNavigationButtonClicked(NavigationButtonType type)
        {
            OnNavigationButtonClicked?.Invoke(type);
        }

        public void HandleLoadSceneButtonClicked(SceneObject obj)
        {
            OnSceneButtonClicked?.Invoke(obj);
        }

        public void SetButtonsPanelSize(float size)
        {
            if (size == 0f)
                _buttonsPanel.SetActive(false);
            else if (!_buttonsPanel.activeSelf)
                _buttonsPanel.SetActive(true);

            _buttonsPanel.transform.localScale = Vector3.one * size;
        }

        public void SetMapsPanelSize(float size)
        {
            if (size == 0f)
                _mapsPanel.SetActive(false);
            else if (!_mapsPanel.activeSelf)
                _mapsPanel.SetActive(true);

            _mapsPanel.transform.localScale = Vector3.one * size;
        }

        private void OnDestroy()
        {
            foreach (var button in _navigationButtons)
            {
                button.OnClicked -= HandleNavigationButtonClicked;
            }
        }

    }
}