#nullable enable

using System;

namespace Feature.Combat.Domain
{
    public interface IReadOnlyWeaponInventory
    {
        IWeapon? GetByWorldId(Guid worldId);
    }
}