using UnityEngine;

namespace Feature.Items.Infrastructure
{
    public interface IWeaponAnimator
    {
        void PlayUseAnimation(int weaponId);
        void PlayReloadAnimation();
        void ForceStopAnimation();
    }
}