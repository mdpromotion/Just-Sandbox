using Feature.Combat.Data;
using UnityEngine;

namespace Feature.Combat.Infrastructure
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