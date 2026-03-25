using Feature.Combat.Data;
using System;

namespace Feature.Combat.Domain
{
    /// <summary>
    /// Defines the contract for a weapon within the game world, providing access to its identity, configuration, type,
    /// and ammunition state.
    /// </summary>
    /// <remarks>Implementations of this interface represent individual weapons and expose properties for
    /// tracking their unique identifiers, configuration, world association, type, and current ammunition levels. This
    /// interface is intended for use in systems that manage or interact with weapons, such as inventory, combat, or
    /// persistence modules.</remarks>
    public interface IWeapon
    {
        Guid Id { get; }
        int ConfigId { get; }
        Guid WorldId { get; }
        WeaponType Type { get; }
        int CurrentAmmo { get; }
        int ReserveAmmo { get; }
    }
}