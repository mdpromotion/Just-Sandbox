using Feature.Combat.Data;
using System;

namespace Feature.Combat.Domain
{
    public class ShootableItem : IWeapon, IUsable, IReloadable
    {
        public Guid Id { get; }
        public int ConfigId { get; }
        public Guid WorldId { get; }
        public float Cooldown { get; }
        public float ReloadCooldown => 1.35f;
        public int MaxAmmoInClip { get; }
        public int ReserveAmmo { get; private set; }
        public int CurrentAmmo { get; private set; }
        public WeaponType Type => WeaponType.Shootable;

        public ShootableItem(
            Guid id,
            int configId,
            Guid worldId,
            float cooldown,
            int maxAmmoInClip,
            int reserveAmmo)
        {
            Id = id;
            ConfigId = configId;
            WorldId = worldId;
            Cooldown = cooldown;
            MaxAmmoInClip = maxAmmoInClip;
            ReserveAmmo = reserveAmmo;
            CurrentAmmo = maxAmmoInClip;
        }

        public Result Use()
        {
            return Shoot();
        }

        public Result Reload()
        {
            if (ReserveAmmo <= 0)
            {
                return Result.Failure("No ammo to reload");
            }

            int needed = MaxAmmoInClip - CurrentAmmo;
            int toReload = Math.Min(needed, ReserveAmmo);

            CurrentAmmo += toReload;
            ReserveAmmo -= toReload;

            return Result.Success();
        }

        private Result Shoot()
        {
            if (CurrentAmmo <= 0)
            {
                return Result.Failure("No ammo");
            }
            CurrentAmmo--;
            return Result.Success();
        }
    }
}