using UnityEngine;

namespace Feature.Inventory.Presentation
{
    public interface IView
    {
        void ToggleIconOutline(int index, bool isActive);
        void SetIcon(int index, Sprite sprite);
    }
}