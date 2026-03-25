using Feature.Combat.Domain;
using System;

namespace Feature.Combat.Infrastructure
{
    public interface IWeaponFactory
    {
        /// <summary>
        /// Creates a new weapon in the specified world using the provided weapon data.
        /// </summary>
        /// <param name="item">The data describing the weapon to create. Cannot be null.</param>
        /// <param name="worldId">The unique identifier of the world in which to create the weapon.</param>
        /// <returns>A result containing the newly created weapon if the operation succeeds; otherwise, a result indicating the
        /// reason for failure.</returns>
        Result<IWeapon> CreateWeapon(IWeaponProvider item, Guid worldId);
    }
}