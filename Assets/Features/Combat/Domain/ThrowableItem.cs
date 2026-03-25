using Feature.Combat.Data;
using System;

namespace Feature.Combat.Domain
{
    public class ThrowableItem : IWeapon, IUsable
    {
        public Guid Id { get; }
        public int ConfigId { get; }
        public Guid WorldId { get; }
        public float Cooldown { get; }
        public int CurrentAmmo { get; private set; }
        public int ReserveAmmo => 0;
        public WeaponType Type => WeaponType.Throwable;

        public ThrowableItem(
            Guid id,
            int configId,
            Guid worldId,   
            int currentAmmo,
            float cooldown)
        {
            Id = id;
            ConfigId = configId;
            WorldId = worldId;
            CurrentAmmo = currentAmmo;
            Cooldown = cooldown;
        }

        public Result Use()
        {
            return Throw();
        }

        private Result Throw()
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