using UnityEngine;

namespace Feature.Combat.Domain
{
    /// <summary>
    /// Defines a contract for objects that support reloading their internal state or configuration.
    /// </summary>
    /// <remarks>Implementations of this interface provide a mechanism to refresh or reload their data,
    /// settings, or resources. This can be useful in scenarios where configuration changes need to be applied at
    /// runtime without recreating the object.</remarks>
    public interface IReloadable
    {
        /// <summary>
        /// Reloads the current configuration or state, applying any changes made since the last load.
        /// </summary>
        /// <returns>A <see cref="Result"/> indicating whether the reload operation was successful. The result contains
        /// information about any errors encountered during the reload.</returns>
        Result Reload();
        float ReloadCooldown { get; }
    }
}