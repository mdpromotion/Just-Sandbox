using Features.Combat.Domain.Interfaces;

namespace Features.Combat.Application.Interfaces
{
    public interface IWeaponShotUseCase
    {
        Result Shoot(IWeapon weapon);
    }
}