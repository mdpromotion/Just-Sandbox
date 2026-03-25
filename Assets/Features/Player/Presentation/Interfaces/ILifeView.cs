using UnityEngine;

namespace Feature.Player.Presentation
{
    public interface ILifeView
    {
        void SetHealth(float percent);
        void SetDeathScreenTransparency(float percent);
        float CurrentFillAmount();
    }
}