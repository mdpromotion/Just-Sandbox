namespace Features.Combat.Presentation.Interfaces
{
    public interface IWeaponAnimator
    {
        void PlayUseAnimation(int weaponId);
        void PlayReloadAnimation();
        void ForceStopAnimation();
    }
}