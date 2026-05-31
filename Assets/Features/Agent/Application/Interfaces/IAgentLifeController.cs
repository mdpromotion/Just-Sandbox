using System;

namespace Features.Agent.Application.Interfaces
{
    /// <summary>
    /// Handles end-of-life operations for agent entities.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface are responsible for initiating whatever cleanup,
    /// removal, or "burial" logic is appropriate for an agent identified by <paramref name="entityId"/>.
    /// This might include releasing resources, unregistering the entity from systems, scheduling
    /// a Unity GameObject for destruction, persisting final state, or performing other teardown work.
    ///
    /// The interface intentionally exposes a simple, synchronous request method. Implementations
    /// may perform the actual work synchronously or schedule it to run asynchronously on the
    /// appropriate execution context (for example, Unity's main thread) — callers should not
    /// assume immediate removal unless documented by a concrete implementation.
    /// </remarks>
    public interface IAgentLifeController
    {
        /// <summary>
        /// Request that the agent with the provided <paramref name="entityId"/> be buried (removed/cleaned up).
        /// </summary>
        /// <param name="entityId">
        /// The unique identifier of the entity to be buried. This value must correspond to an existing
        /// agent entity managed by the system; if no entity with this id exists the implementation
        /// should handle that case gracefully (for example: ignore, log a warning, or return an error
        /// through an out-of-band mechanism).
        /// </param>
        /// <remarks>
        /// - Implementations should aim for idempotence: calling <see cref="RequestBurial"/> multiple
        ///   times for the same <paramref name="entityId"/> should not cause errors or duplicate work.
        /// - Prefer non-blocking behavior: if burial requires heavy work, schedule it rather than blocking
        ///   the caller. If immediate confirmation is required, a concrete implementation can provide
        ///   an asynchronous alternative.
        /// - In Unity contexts, ensure any GameObject destruction or component access happens on the main
        ///   thread to avoid threading issues.
        /// </remarks>
        void RequestBurial(Guid entityId);
    }
}