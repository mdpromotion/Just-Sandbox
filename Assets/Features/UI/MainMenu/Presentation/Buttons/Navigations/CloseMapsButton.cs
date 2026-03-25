using UnityEngine;
using UnityEngine.UI;

namespace Feature.MainMenu.Presentation
{
    [RequireComponent(typeof(Button))]
    public class CloseMapsButton : NavigationButton
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            NavigationButtonType = NavigationButtonType.CloseMaps;
            _button.onClick.AddListener(OnCloseMapsButtonClicked);
        }

        private void OnCloseMapsButtonClicked()
        {
            print("CloseMapsButton clicked");
            print("Invoking OnNavigationButtonClicked event with type: " + NavigationButtonType);
            InvokeClicked();
        }
    }
}