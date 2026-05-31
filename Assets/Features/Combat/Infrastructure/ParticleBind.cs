using UnityEngine;

namespace Features.Combat.Infrastructure
{
    public class ParticleBind : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particleSystem;

        public void PlayParticleEffect()
        {
            if (particleSystem != null)
            {
                particleSystem.Emit(1);
            }
        }
    }
}