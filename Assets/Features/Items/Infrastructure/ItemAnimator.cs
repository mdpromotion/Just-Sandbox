using UnityEngine;

namespace Feature.Items.Infrastructure
{
    [RequireComponent(typeof(Animator))]
    public class ItemAnimator : MonoBehaviour, IItemAnimator, IWeaponAnimator
    {
        private Animator _animator;

        public void Awake()
        {
            if (TryGetComponent(out Animator animator))
                _animator = animator;
        }

        public void PlayUseAnimation(int weaponId)
        {
            SetWeaponId(weaponId);
            StartAnimation("Use");
        }

        public void PlayReloadAnimation()
            => StartAnimation("Reload");

        public void PlayEquipAnimation()
            => StartAnimation("Equip");

        public void StopAnimation()
            => _animator.SetBool("IsUsing", false);

        public void ForceStopAnimation()
        {
            _animator.SetBool("IsUsing", false);

            ResetTriggers();

            _animator.Update(0f);
        }

        public void RebindAnimator()
        {
            _animator.Rebind();
            _animator.Update(0f);
        }

        private void SetWeaponId(int weaponId)
            => _animator.SetInteger("WeaponId", weaponId);

        private void StartAnimation(string animationName)
        {
            _animator.SetBool("IsUsing", true);
            _animator.SetTrigger(animationName);
        }

        private void ResetTriggers()
        {
            _animator.ResetTrigger("Use");
            _animator.ResetTrigger("Reload");
            _animator.ResetTrigger("Equip");
            _animator.ResetTrigger("Unequip");
        }
    }
}