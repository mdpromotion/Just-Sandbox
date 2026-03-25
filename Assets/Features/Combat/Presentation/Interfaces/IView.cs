using UnityEngine;

namespace Feature.Combat.Presentation
{
    public interface IView
    {
        void SetAmmoText(string text);
        void ToggleAmmoText(bool enabled);
    }
}