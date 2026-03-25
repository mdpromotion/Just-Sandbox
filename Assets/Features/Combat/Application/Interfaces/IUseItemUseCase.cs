using Feature.Combat.Domain;

namespace Feature.Combat.Application
{
    public interface IUseItemUseCase
    {
        Result<IWeapon> Use();
        Result<IWeapon> Reload();
    }
}