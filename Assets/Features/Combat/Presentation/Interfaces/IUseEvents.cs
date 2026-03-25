using Feature.Combat.Domain;
using System;
using UnityEngine;

namespace Feature.Combat.Application
{
    public interface IUseEvents
    {
        event Action<IWeapon> Used;
        event Action<IWeapon> Reloaded;
    }
}