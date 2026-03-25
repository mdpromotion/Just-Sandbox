#nullable enable
using Shared.Domain;

namespace Feature.Combat.Infrastructure
{
    public interface IWeaponService
    {
        ITarget? GetTarget(float distance);
    }
}