using System;
using UnityEngine;

namespace Feature.Combat.Application
{
    public interface IWeaponItemUseCase
    {
        void SpawnWeapon(IWeaponProvider weaponData, Guid worldId);
    }
}