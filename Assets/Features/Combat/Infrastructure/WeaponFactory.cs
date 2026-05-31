using System;
using Features.Combat.Application.Interfaces;
using Features.Combat.Data;
using Features.Combat.Domain;
using Features.Combat.Domain.Interfaces;

namespace Features.Combat.Infrastructure
{
    public class WeaponFactory : IWeaponFactory
    {
        public Result<IWeapon> CreateWeapon(IWeaponProvider item, Guid worldId)
        {
            var id = Guid.NewGuid();

            IWeapon weapon;

            switch (item.WeaponType)
            {
                case WeaponType.Shootable:
                    weapon = new ShootableItem(id, item.Id, worldId, item.Cooldown, item.MaxAmmoInClip, item.ReserveAmmo);
                    break;
                case WeaponType.Throwable:
                    weapon = new ThrowableItem(id, item.Id, worldId, item.ReserveAmmo, item.Cooldown);
                    break;
                default:
                    return Result<IWeapon>.Failure($"Unsupported weapon type: {item.WeaponType}");
            }

            return Result<IWeapon>.Success(weapon);
        }
    }
}