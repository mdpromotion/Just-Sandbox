using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Feature.Toolbox.Presentation.Buttons
{
    [RequireComponent(typeof(Button))]
    public class SpawnModeButtonView : MonoBehaviour
    {
        public event Action Clicked;

        private Button _button;

        [Inject]
        public void Constuct()
        {
            if (TryGetComponent<Button>(out var button))
            {
                _button = button;
                _button.onClick.AddListener(() => Clicked?.Invoke());
            }
        }

        public void ChangeColor(Color color)
        {
            if (_button != null)
                _button.image.color = color;
        }
    }
}