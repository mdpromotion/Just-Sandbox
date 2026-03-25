using System;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.PlayerExitMenu.Presentation
{
    public class View : MonoBehaviour, IView
    {
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private List<ExitMenuButton> _buttons;

        public event Action<NavigationButtonType> OnNavigationButtonClicked;

        private void Awake()
        {
            foreach (var button in _buttons) 
            {
                button.OnClicked += OnButtonClicked;
            }
        }

        private void OnButtonClicked(NavigationButtonType type)
        {
            print($"Button {type} clicked");
            OnNavigationButtonClicked?.Invoke(type);
        }

        public void ToggleMenu(bool state)
        {
            _menuPanel.SetActive(state);
        }
        public void SetMenuPanelSize(float size)
        {
            if (size == 0f)
                _menuPanel.SetActive(false);
            else if (!_menuPanel.activeSelf)
                _menuPanel.SetActive(true);

            _menuPanel.transform.localScale = Vector3.one * size;
        }
    }
}