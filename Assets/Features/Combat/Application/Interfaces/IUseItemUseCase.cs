using Features.Combat.Domain.Interfaces;

namespace Features.Combat.Application.Interfaces
{
    public interface IUseItemUseCase
    {
        Result<IWeapon> Use();
        Result<IWeapon> Reload();
    }
}