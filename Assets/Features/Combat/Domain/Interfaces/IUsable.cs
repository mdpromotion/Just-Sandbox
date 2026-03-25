using UnityEngine;

namespace Feature.Combat.Domain
{
    /// <summary>
    /// Defines a contract for objects that can be used to perform an action or operation.
    /// </summary>
    /// <remarks>Implement this interface to provide custom behavior when the object is used. The specific effect of
    /// calling the Use method depends on the implementation.</remarks>
    public interface IUsable
    {
        /// <summary>
        /// Executes the operation and returns the result.
        /// </summary>
        /// <returns>A <see cref="Result"/> object containing the outcome of the operation.</returns>
        Result Use();
        float Cooldown { get; }
    }
}