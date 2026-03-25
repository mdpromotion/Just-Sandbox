using Feature.Combat.Domain;
using UnityEngine;

namespace Feature.Combat.Application
{
    public interface IWeaponShotUseCase
    {
        Result Shoot(IWeapon weapon);
    }
}