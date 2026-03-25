using UnityEngine;
using UnityEngine.UI;

namespace Feature.Inventory.Presentation
{
    public class View : MonoBehaviour, IView
    {
        [SerializeField] private Image[] _inventoryContainers;
        [SerializeField] private GameObject[] _inventoryIconsOutline;

        public void ToggleIconOutline(int index, bool isActive)
        {
            if (index < 0 || index >= _inventoryIconsOutline.Length)
                return;

            _inventoryIconsOutline[index].SetActive(isActive);
        }

        public void SetIcon(int index, Sprite sprite)
        {
            if (index < 0 || index >= _inventoryIconsOutline.Length)
                return;

            var iconImage = _inventoryContainers[index];
            if (iconImage != null)
            {
                iconImage.sprite = sprite;
            }
        }
    }
}