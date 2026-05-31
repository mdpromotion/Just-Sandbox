using Shared.Data;

namespace Features.Agent.Domain.Interfaces
{
    public interface IAgentController
    {
        Position3 AgentPosition { get; }
        void MoveTowardsTarget();
        void StartMovement();
        void StopMovement();
        void Punch(Position3 velocity);
        void Die(Position3 velocity);
        void Tick();
    }
}