using Feature.UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.UI.Maps.Presentation
{
    [RequireComponent(typeof(Button))]
    public class CloseMapsButton : NavigationButton
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnCloseMapsButtonClicked);
        }

        private void OnCloseMapsButtonClicked()
        {
            InvokeClicked();
        }
    }
}