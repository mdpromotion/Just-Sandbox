using Feature.Combat.Domain;
using Feature.Inventory.Domain;
using Shared.Providers;
using System;

namespace Feature.Combat.Application
{
    public class UseItemUseCase : IUseItemUseCase
    {
        private readonly IReadOnlyWeaponInventory _weaponInventory;
        private readonly IReadOnlyPlayerInventory _playerInventory;
        private readonly ICooldownService _cooldown;
        private readonly ITimeProvider _timeProvider;

        public UseItemUseCase(
            IReadOnlyWeaponInventory weaponInventory,
            IReadOnlyPlayerInventory playerInventory,
            ICooldownService cooldown,
            ITimeProvider timeProvider)
        {
            _weaponInventory = weaponInventory;
            _playerInventory = playerInventory;
            _cooldown = cooldown;
            _timeProvider = timeProvider;
        }

        public Result<IWeapon> Use()
        {
            var result = GetCurrentWeapon();
            if (!result.IsSuccess)
                return Result<IWeapon>.Failure("Cannot find any weapon in player's hand");

            IWeapon weapon = result.Value;

            if (weapon is IUsable usableItem)
            {
                if (!_cooldown.IsAvaliable(_timeProvider.Now))
                    return Result<IWeapon>.Failure("Cooldown");

                var use = usableItem.Use();
                if (!use.IsSuccess)
                    return Result<IWeapon>.Failure(use.Error);

                _cooldown.UpdateLastUseTime(_timeProvider.Now, usableItem.Cooldown);
                return Result<IWeapon>.Success(weapon);
            }
            else
            {
                return Result<IWeapon>.Failure($"Selected item with world ID {weapon.WorldId} is not usable.");
            }
        }

        public Result<IWeapon> Reload()
        {
            var result = GetCurrentWeapon();
            if (!result.IsSuccess)
                return Result<IWeapon>.Failure(result.Error);

            IWeapon weapon = result.Value;

            if (weapon is IReloadable reloadableItem)
            {
                if (!_cooldown.IsAvaliable(_timeProvider.Now))
                    return Result<IWeapon>.Failure("Cooldown");

                var reload = reloadableItem.Reload();
                if (!reload.IsSuccess)
                    return Result<IWeapon>.Failure(reload.Error);

                _cooldown.UpdateLastUseTime(_timeProvider.Now, reloadableItem.ReloadCooldown);

                return Result<IWeapon>.Success(weapon);
            }
            else
            {
                return Result<IWeapon>.Failure($"Selected item with world ID {weapon.WorldId} is not reloadable.");
            }
        }

        private Result<IWeapon> GetCurrentWeapon()
        {
            Guid worldId = _playerInventory.GetSelectedWorldId();
            if (worldId == Guid.Empty)
            {
                return Result<IWeapon>.Failure("No item selected in inventory.");
            }

            var weapon = _weaponInventory.GetByWorldId(worldId);
            if (weapon == null)
            {
                return Result<IWeapon>.Failure($"No weapon found in inventory for world ID {worldId}.");
            }

            return Result<IWeapon>.Success(weapon);
        }
    }
}