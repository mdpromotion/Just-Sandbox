using UnityEngine;
using UnityEngine.UI;

namespace Feature.MapsMenu.Presentation
{
    [RequireComponent(typeof(Button))]
    public class OpenMapsButton : NavigationButton
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            NavigationButtonType = NavigationButtonType.OpenMaps;
            _button.onClick.AddListener(OnOpenMapsButtonClicked);
        }

        private void OnOpenMapsButtonClicked() 
        {
            InvokeClicked();
        }
    }
}
