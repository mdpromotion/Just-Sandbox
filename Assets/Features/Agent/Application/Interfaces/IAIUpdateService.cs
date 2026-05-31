using System;
using Feature.Agent.Application;
using Features.Agent.Domain.Interfaces;
using Features.Agent.Infrastructure.Services.Interfaces;

namespace Features.Agent.Application.Interfaces
{
    /// <summary>
    /// Service that manages agents which participate in AI updates.
    /// </summary>
    /// <remarks>
    /// Implementations track active agents (by <see cref="Guid"/> entity id) and ensure the
    /// associated <see cref="IAgentController"/> and <see cref="IDamageController"/> are
    /// considered during periodic AI/update ticks. Typical responsibilities include:
    /// - registering agents so they are iterated by the AI update loop,
    /// - removing agents when they are no longer active,
    /// - ensuring thread-safety and main-thread execution for Unity-related operations.
    ///
    /// Implementations should document their concurrency and lifecycle guarantees (for example,
    /// whether registration may be performed from background threads or must be performed on the
    /// Unity main thread). Prefer idempotent semantics where reasonable: repeated RegisterAgent
    /// calls for the same entity should not create duplicate processing entries.
    /// </remarks>
    public interface IAIUpdateService
    {
        /// <summary>
        /// Register an agent so it participates in AI updates.
        /// </summary>
        /// <param name="entityId">
        /// The unique identifier of the agent entity. This id is used to later identify and
        /// unregister the agent. Implementations expect this id to be stable for the lifetime
        /// of the agent registration.
        /// </param>
        /// <param name="controller">
        /// The <see cref="IAgentController"/> that exposes the agent's behavior and update hooks.
        /// The service will call into this controller during AI/update cycles.
        /// </param>
        /// <param name="damageController">
        /// The <see cref="IDamageController"/> that exposes damage-related state or events for the agent.
        /// The AI update logic may query or subscribe to this controller to react to damage or health changes.
        /// </param>
        /// <remarks>
        /// - Implementations should validate inputs and handle null references gracefully (either by
        ///   throwing a documented exception or by ignoring the registration and logging).
        /// - If an agent with the same <paramref name="entityId"/> is already registered, implementations
        ///   should either update the stored controllers to the new instances or ignore the call — this
        ///   behavior should be documented by the concrete implementation.
        /// - Registration should be efficient and safe to call during runtime (for example, from spawn
        ///   code paths). If heavy initialization is required, consider performing it asynchronously.
        /// </remarks>
        void RegisterAgent(Guid entityId, IAgentController controller, IDamageController damageController);

        /// <summary>
        /// Unregister an agent so it no longer participates in AI updates.
        /// </summary>
        /// <param name="entityId">
        /// The unique identifier of the agent to remove from AI processing. If the id is not found,
        /// implementations should handle the call gracefully (for example, ignore it or log a warning).
        /// </param>
        /// <remarks>
        /// - This method should be idempotent: calling it multiple times for the same id should not
        ///   cause errors or unexpected behavior.
        /// - Ensure any subscriptions or references held by the service are released to avoid leaks.
        /// - If UnregisterAgent must run on a specific thread (e.g., Unity main thread), document that
        ///   requirement and provide a safe way to schedule the removal if called from other threads.
        /// </remarks>
        void UnregisterAgent(Guid entityId);
    }
}