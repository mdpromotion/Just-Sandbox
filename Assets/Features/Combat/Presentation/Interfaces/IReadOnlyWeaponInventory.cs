#nullable enable

using System;
using Features.Combat.Domain.Interfaces;

namespace Features.Combat.Presentation.Interfaces
{
    public interface IReadOnlyWeaponInventory
    {
        IWeapon? GetByWorldId(Guid worldId);
    }
}