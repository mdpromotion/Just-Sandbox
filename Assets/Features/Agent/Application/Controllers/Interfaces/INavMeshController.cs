using Shared.Data;

namespace Features.Agent.Application.Controllers.Interfaces
{
    public interface INavMeshController
    {
        Position3 Position { get; }
        void Punch(Position3 position);
        void Die(Position3 position);
        void SetSpeed(float speed);
        void SetDestination(Position3 destination);
        void StartMovement();
        void StopMovement();
    }
}