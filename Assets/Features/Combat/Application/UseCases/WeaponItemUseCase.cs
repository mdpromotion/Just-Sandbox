using Feature.Combat.Domain;
using Feature.Combat.Infrastructure;
using System;
using UnityEngine;

namespace Feature.Combat.Application
{
    public class WeaponItemUseCase : IWeaponItemUseCase
    {
        public static readonly string LogTag = nameof(WeaponItemUseCase);

        private readonly WeaponInventory _weaponInventory;
        private readonly IWeaponFactory _weaponFactory;
        private readonly ILogger _logger;

        public WeaponItemUseCase(
            WeaponInventory inventory,
            ILogger logger,
            IWeaponFactory weaponFactory)
        {
            _weaponInventory = inventory;
            _logger = logger;
            _weaponFactory = weaponFactory;
        }

        public void SpawnWeapon(IWeaponProvider weaponData, Guid worldId)
        {
            var weaponResult = _weaponFactory.CreateWeapon(weaponData, worldId);
            if (!weaponResult.IsSuccess)
            {
                _logger.LogError(LogTag, weaponResult.Error);
                return;
            }

            var result = _weaponInventory.Add(weaponResult.Value);
            if (!result.IsSuccess)
            {
                _logger.LogError(LogTag, result.Error);
                return;
            }
        }
    }
}