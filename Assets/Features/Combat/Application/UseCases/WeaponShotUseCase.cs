using Feature.Combat.Domain;
using Feature.Combat.Infrastructure;
using Feature.Toolbox.Infrastructure;
using Shared.Data;
using Shared.Providers;

namespace Feature.Combat.Application
{
    public class WeaponShotUseCase : IWeaponShotUseCase
    {
        private readonly IWeaponService _weaponService;
        private readonly IItemConfigService _config;
        private readonly IPlayerTransformData _player;

        private readonly float _shotDistance;

        public WeaponShotUseCase(
            IWeaponService weaponService,
            IItemConfigService config,
            IPlayerTransformData player,
            float shotDistance = 100f)
        {
            _weaponService = weaponService;
            _config = config;
            _player = player;
            _shotDistance = shotDistance;
        }

        public Result Shoot(IWeapon weapon)
        {
            var configResult = _config.GetItemConfig(weapon.ConfigId);
            if (!configResult.IsSuccess)
                return Result.Failure("Weapon config not found.");

            var target = _weaponService.GetTarget(_shotDistance);
            if (target == null)
                return Result.Failure("No targets.");

            if (configResult.Value is not IWeaponProvider config)
                return Result.Failure("Current item is not a weapon.");

            var damageResult = target.ReceiveDamage(new AttackInfo(config.Damage, config.Knockback, _player.Position));
            if (!damageResult.IsSuccess)
                return Result.Failure("Failed to apply damage to target.");

            return Result.Success();
        }
}
}