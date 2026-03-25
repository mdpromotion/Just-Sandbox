using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class TextureButtonView : MonoBehaviour
{
    [SerializeField] private int _index;
    public event Action<int> Clicked;

    private Button _button;
    private Image _mark = null;

    [Inject]
    public void Construct()
    {
        if (TryGetComponent<Button>(out var button))
        {
            _button = button;
            _button.onClick.AddListener(() => Clicked?.Invoke(_index));
        }

        Transform child = transform.Find("ActiveMark");
        if (child != null)
        {
            _mark = child.GetComponent<Image>();
        }
    }
    public void ToggleMark(bool enabled)
    {
        if (_mark != null)
            _mark.gameObject.SetActive(enabled);
    }
}