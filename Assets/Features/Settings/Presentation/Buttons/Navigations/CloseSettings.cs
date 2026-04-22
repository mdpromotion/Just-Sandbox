using Feature.UI.Shared;
using UnityEngine.UI;

namespace Feature.UI.Settings.Presentation
{
    public class CloseSettings : NavigationButton
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