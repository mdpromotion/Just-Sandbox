using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Feature.Toolbox.Presentation.Buttons
{
    [RequireComponent(typeof(Button))]
    public class SpawnButtonView : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private SpawnCategory _category;
        public event Action<SpawnButtonData> Clicked;

        private Button _button;

        [Inject]
        public void Constuct()
        {
            if (TryGetComponent<Button>(out var button))
            {
                _button = button;
                _button.onClick.AddListener(() => Clicked?.Invoke(new SpawnButtonData(_id, _category)));
            }
        }
    }
}