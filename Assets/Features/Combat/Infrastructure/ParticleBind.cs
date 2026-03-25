using UnityEngine;

namespace Feature.Combat.Infrastructure
{
    public class ParticleBind : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public void PlayParticleEffect()
        {
            if (_particleSystem != null)
            {
                _particleSystem.Emit(1);
            }
        }
    }
}