using Feature.UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.UI.Maps.Presentation
{
    [RequireComponent(typeof(Button))]
    public class OpenMapsButton : NavigationButton
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnOpenMapsButtonClicked);
        }

        private void OnOpenMapsButtonClicked() 
        {
            InvokeClicked();
        }
    }
}
