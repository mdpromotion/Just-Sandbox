using Feature.Combat.Data;
using Feature.Items.Infrastructure;

namespace Feature.Combat.Infrastructure
{
    public class WeaponProvider : ItemProvider, IWeaponProvider
    {
        private readonly WeaponData _data;

        public int Damage => _data.Damage;
        public float Range => _data.Range;
        public float Cooldown => _data.Cooldown;
        public float Knockback => _data.Knockback;
        public int MaxAmmoInClip => _data.MaxAmmoInClip;
        public int ReserveAmmo => _data.ReserveAmmo;
        public WeaponType WeaponType => _data.WeaponType;

        public WeaponProvider(WeaponData data) : base(data)
        {
            _data = data;
        }
    }
}