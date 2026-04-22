using Feature.UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.UI.Settings.Presentation
{
    [RequireComponent(typeof(Button))]
    public class OpenSettings : NavigationButton
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnOpenSettingsButtonClicked);
        }

        private void OnOpenSettingsButtonClicked()
        {
            InvokeClicked();
        }
    }
}