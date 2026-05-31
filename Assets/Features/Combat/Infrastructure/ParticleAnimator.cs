using Features.Combat.Presentation.Interfaces;
using UnityEngine;
using Zenject;

namespace Features.Combat.Infrastructure
{
    public class ParticleAnimator : IParticleAnimator
    {
        private readonly Transform _handTransform;

        public ParticleAnimator(
            [Inject(Id = "HandParent")] Transform handTransform)
        {
            _handTransform = handTransform;
        }

        public void PlayParticleEffect()
        {
            var transform = _handTransform.GetChild(0);
            if (transform.TryGetComponent<ParticleBind>(out var particle))
            {
                particle.PlayParticleEffect();
            }
        }
    }
}