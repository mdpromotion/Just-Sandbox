using Feature.Combat.Data;
using Feature.Combat.Domain;
using System;
using UnityEngine;

namespace Feature.Combat.Infrastructure
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