using System;
using Features.Combat.Domain.Interfaces;

namespace Features.Combat.Presentation.Interfaces
{
    public interface IUseEvents
    {
        event Action<IWeapon> Used;
        event Action<IWeapon> Reloaded;
    }
}