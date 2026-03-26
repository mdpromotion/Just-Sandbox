using Feature.Scene.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.MapsMenu.Presentation
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        private Button _button;
        [SerializeField] private SceneObject _sceneObject;
        public event System.Action<SceneObject> OnClicked;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(InvokeClick);
        }

        public void InvokeClick()
        {
            OnClicked?.Invoke(_sceneObject);
        }
    }
}