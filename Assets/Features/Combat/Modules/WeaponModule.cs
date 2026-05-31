using UnityEngine;

// This class is deprecated. Use ParticleBind instead.
namespace Features.Combat.Modules
{
    public class WeaponModule : MonoBehaviour
    {
        public ParticleSystem particles;

        public void ParticleShoot()
        {
            if (particles != null)
            {
                particles.Emit(1);
            }
        }
    }
}