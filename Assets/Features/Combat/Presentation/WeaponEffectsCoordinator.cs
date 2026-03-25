using Feature.Combat.Application;
using Feature.Combat.Domain;
using Feature.Combat.Infrastructure;
using Feature.Items.Infrastructure;
using System;
using Zenject;

namespace Feature.Combat.Presentation
{
    public class WeaponEffectsCoordinator : IInitializable, IDisposable
    {
        private readonly IWeaponAnimator _animator;
        private readonly IParticleAnimator _particle;
        private readonly IUseEvents _events;
        private readonly IAudioPlayer _audio;

        public WeaponEffectsCoordinator(
            IWeaponAnimator animator,
            IParticleAnimator particle,
            IUseEvents events,
            IAudioPlayer audio)
        {
            _animator = animator;
            _particle = particle;
            _events = events;
            _audio = audio;
        }

        public void Initialize()
        {
            _events.Used += OnUsed;
            _events.Reloaded += OnReloaded;
        }

        private void OnUsed(IWeapon weapon)
        {
            _animator.PlayUseAnimation(weapon.ConfigId);
            _audio.PlayOneShot("Shot");
            _particle.PlayParticleEffect();
        }

        private void OnReloaded(IWeapon weapon)
        {
            _animator.PlayReloadAnimation();
            _audio.PlayOneShot("Reload");
        }
        public void Dispose()
        {
            _events.Used -= OnUsed;
            _events.Reloaded -= OnReloaded;
        }
    }
}