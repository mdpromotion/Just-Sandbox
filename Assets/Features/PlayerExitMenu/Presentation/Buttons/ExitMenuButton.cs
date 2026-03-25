using UnityEngine;
using UnityEngine.UI;

namespace Feature.PlayerExitMenu.Presentation
{
    public enum NavigationButtonType { Continue, Exit }

    [RequireComponent(typeof(Button))]
    public class ExitMenuButton : MonoBehaviour
    {
        [SerializeField] private NavigationButtonType _type;
        private Button _button;
        
        public event System.Action<NavigationButtonType> OnClicked;

        public void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            OnClicked?.Invoke(_type);
        }
    }
}