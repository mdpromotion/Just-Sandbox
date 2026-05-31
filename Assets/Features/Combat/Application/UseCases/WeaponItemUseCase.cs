using System;
using Feature.Combat.Application;
using Features.Combat.Application.Interfaces;
using Features.Combat.Domain;
using UnityEngine;

namespace Features.Combat.Application.UseCases
{
    public class WeaponItemUseCase : IWeaponItemUseCase
    {
        public const string LogTag = nameof(WeaponItemUseCase);

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