using System;
using Core.Data;
using Feature.Combat.Application;
using Features.Combat.Application.Interfaces;
using Features.Combat.Domain.Interfaces;
using Features.Combat.Infrastructure.Interfaces;
using Features.Combat.Presentation.Interfaces;
using UnityEngine;

namespace Features.Combat.Application.UseCases
{
    public class UseItemOrchestrator : IUseItemInput, IUseEvents
    {
        public const string LogTag = nameof(UseItemOrchestrator);

        private readonly IUseItemUseCase _useItem;
        private readonly IWeaponShotUseCase _useWeapon;
        private readonly IReadOnlyCoreGameStates _gameStates;
        private readonly ILogger _logger;

        public event Action<IWeapon> Used;
        public event Action<IWeapon> Reloaded;

        public UseItemOrchestrator(
            IUseItemUseCase useItem,
            IWeaponShotUseCase useWeapon,
            IReadOnlyCoreGameStates gameStates,
            ILogger logger)
        {
            _useItem = useItem;
            _useWeapon = useWeapon;
            _gameStates = gameStates;
            _logger = logger;
        }

        public void Use()
        {
            if (!_gameStates.IsPlayerControllable)
                return;

            var itemResult = _useItem.Use();
            if (!itemResult.IsSuccess)
            {
                _logger.LogWarning(LogTag, itemResult.Error);
                return;
            }

            Used?.Invoke(itemResult.Value);

            var weaponResult = _useWeapon.Shoot(itemResult.Value);
            if (!weaponResult.IsSuccess)
            {
                _logger.LogWarning(LogTag, weaponResult.Error);
                return;
            }
        }
        public void Reload()
        {
            if (!_gameStates.IsPlayerControllable)
                return;

            var itemResult = _useItem.Reload();
            if (!itemResult.IsSuccess)
            {
                _logger.LogWarning(LogTag, itemResult.Error);
                return;
            }

            Reloaded?.Invoke(itemResult.Value);

        }

    }
}