using Feature.Agent.Infrastructure;
using Features.Agent.Data;
using UnityEngine;

namespace Features.Agent.Application.Interfaces
{
    /// <summary>
    /// Contract for an assembler that constructs/configures an "agent" on a Unity <see cref="GameObject"/>.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface are responsible for creating and wiring up the runtime parts
    /// of an agent (components, child objects, bindings, etc.) using information supplied by an
    /// <see cref="IAgentProvider"/>. The method returns a <see cref="Result{T}"/> containing a
    /// <see cref="FactoryOutput"/> describing the constructed agent on success, or a failed result
    /// carrying error information on failure.
    ///
    /// Implementations should prefer returning a failed <see cref="Result{T}"/> to throwing exceptions
    /// for expected error conditions (for example: missing configuration, invalid provider data,
    /// null target object). Exceptions may still be thrown for truly exceptional/unrecoverable states.
    /// </remarks>
    public interface IAgentAssembler
    {
        /// <summary>
        /// Create and configure an agent on the provided <paramref name="obj"/> using the supplied
        /// <paramref name="agentProvider"/>.
        /// </summary>
        /// <param name="agentProvider">
        /// Provider that supplies agent-specific factories, configuration or data required during assembly.
        /// Implementations typically query this provider to resolve components, scripts or data objects
        /// needed to build the agent.
        /// </param>
        /// <param name="obj">
        /// The Unity <see cref="GameObject"/> that will host the assembled agent. The object is expected
        /// to be non-null and may be modified by the assembler (components added, properties changed,
        /// children instantiated). If <c>null</c>, implementations should return a failed <see cref="Result{T}"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Result{FactoryOutput}"/> describing the outcome. On success the result contains
        /// a <see cref="FactoryOutput"/> instance with details (for example: created components, handles,
        /// or identifiers). On failure the result contains error information explaining why assembly failed.
        /// </returns>
        Result<FactoryOutput> CreateAgent(IAgentProvider agentProvider, GameObject obj);
    }
}