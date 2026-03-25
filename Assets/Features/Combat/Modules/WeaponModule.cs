using UnityEngine;

namespace Weapon
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