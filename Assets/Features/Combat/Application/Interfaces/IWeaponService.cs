#nullable enable
using Shared.Domain;

namespace Features.Combat.Application.Interfaces
{
    public interface IWeaponService
    {
        ITarget? GetTarget(float distance);
    }
}