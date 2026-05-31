using System;

namespace Features.Agent.Application.Interfaces
{
    /// <summary>
    /// Service that tracks attack cooldowns for agents.
    /// </summary>
    /// <remarks>
    /// Implementations are responsible for storing the last-attack timestamps for agent ids
    /// and answering whether an agent is allowed to perform an attack at a given time.
    /// Times are represented as <see cref="float"/> values (commonly Unity seconds like <c>Time.time</c>).
    ///
    /// Implementations should document/thread-safety and whether times must be monotonic. Typical
    /// semantics are:
    /// - If no record exists for an id, <see cref="CanAttack(Guid, float)"/> returns <c>true</c>.
    /// - <see cref="UpdateAttackTime(Guid, float, float)"/> records that an attack happened at
    ///   <paramref name="now"/> and applies the supplied <paramref name="cooldown"/> so subsequent
    ///   calls to <see cref="CanAttack(Guid, float)"/> will return <c>false</c> until the expiry.
    /// </remarks>
    public interface ICooldownService
    {
        /// <summary>
        /// Determine whether the agent identified by <paramref name="id"/> is allowed to attack at time <paramref name="now"/>.
        /// </summary>
        /// <param name="id">Unique identifier of the agent whose cooldown is being queried.</param>
        /// <param name="now">
        /// The current time value in seconds. This value should use the same time base that was supplied
        /// to <see cref="UpdateAttackTime(Guid, float, float)"/> (for example, Unity's <c>Time.time</c>).
        /// </param>
        /// <returns>
        /// <c>true</c> if the agent may perform an attack at <paramref name="now"/>; otherwise <c>false</c>.
        /// Typical semantics:
        /// - If the agent has no prior attack recorded, returns <c>true</c>.
        /// - If the agent attacked at time t and the cooldown was c, returns <c>true</c> when <paramref name="now"/> >= t + c.
        /// </returns>
        /// <remarks>
        /// - Implementations should treat small floating-point imprecision robustly (for example, by
        ///   using a small epsilon when comparing times if required).
        /// - If callers supply times from different bases (for example, mixing realtime and game-time),
        ///   results will be undefined; callers must be consistent.
        /// </remarks>
        bool CanAttack(Guid id, float now);

        /// <summary>
        /// Record that the agent identified by <paramref name="id"/> performed an attack at time <paramref name="now"/>,
        /// and set the cooldown duration for when the next attack is allowed.
        /// </summary>
        /// <param name="id">Unique identifier of the agent whose attack time is being updated.</param>
        /// <param name="now">
        /// The time at which the attack occurred, in seconds (same time base as used by <see cref="CanAttack(Guid, float)"/>).
        /// </param>
        /// <param name="cooldown">
        /// The cooldown duration in seconds that must elapse after <paramref name="now"/> before the agent can attack again.
        /// A non-negative value is expected; behavior for negative cooldowns should be documented by the implementation
        /// (commonly treated as zero).
        /// </param>
        /// <remarks>
        /// - Implementations should make this operation efficient; it is typically called on spawn or when an attack happens.
        /// - This method is commonly called immediately after <see cref="CanAttack(Guid, float)"/> returns <c>true</c>.
        /// - Implementations should ensure idempotence where appropriate: repeatedly calling this with the same values
        ///   should result in the same state.
        /// - If the implementation stores per-id state, it should also consider eviction policies to avoid unbounded memory growth.
        /// </remarks>
        void UpdateAttackTime(Guid id, float now, float cooldown);
    }
}