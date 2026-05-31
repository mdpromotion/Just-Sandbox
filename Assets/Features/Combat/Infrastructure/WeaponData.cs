using Features.Combat.Data;
using UnityEngine;

namespace Features.Combat.Infrastructure
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Weapon")]
    public class WeaponData : ItemData
    {
        public int Damage;
        public float Range;
        public float Cooldown;
        public float Knockback;
        public int MaxAmmoInClip;
        public int ReserveAmmo;
        public WeaponType WeaponType;
    }
}