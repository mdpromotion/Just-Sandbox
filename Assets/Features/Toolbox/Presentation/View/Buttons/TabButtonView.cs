using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Feature.Toolbox.Presentation.Buttons
{
    [RequireComponent(typeof(Button))]
    public class TabButtonView : MonoBehaviour
    {
        [SerializeField] private int _index;
        public event Action<int> Clicked;

        private Button _button;

        [Inject]
        public void Construct()
        {
            if (TryGetComponent<Button>(out var button))
            {
                _button = button;
                _button.onClick.AddListener(() => Clicked?.Invoke(_index));
            }
        }
        public void ChangeColor(Color color)
        {
            _button.image.color = color;
        }
    }
}