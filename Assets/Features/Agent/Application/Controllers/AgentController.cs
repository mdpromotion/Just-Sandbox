using Core.Data;
using Feature.Agent.Domain;
using Feature.Agent.Infrastructure;
using Shared.Data;

namespace Feature.Agent.Application
{
    /// <summary>
    /// Controls the behavior and actions of an agent within the game world, managing its state and interactions with
    /// entities.
    /// </summary>
    /// <remarks>The AgentController utilizes a finite state machine (FSM) to determine the agent's current
    /// state and behavior. It interacts with the IWorldEntityService to find nearby entities and uses a navigation mesh
    /// controller to manage movement. The agent's vision range can be configured during instantiation, affecting its
    /// ability to detect nearby entities.</remarks>
    public class AgentController : IAgentController
    {
        private readonly NavigationController _navigationController;
        private readonly IReadOnlyCoreGameStates _gameState;
        private readonly INavMeshController _controller;
        private readonly AgentFSM _fsm;

        public Position3 AgentPosition => _controller.Position;
        private Position3 _targetPosition = Position3.Zero;

        public AgentController(
            NavigationController navigationController,
            IReadOnlyCoreGameStates gameState,
            INavMeshController controller,
            AgentFSM fsm)
        {
            _navigationController = navigationController;
            _gameState = gameState;
            _controller = controller;
            _fsm = fsm;
        }

        public void Tick()
        {
            if (!_gameState.IsPlayerControllable)
            {
                StopMovement();
                return;
            }

            StartMovement();
            _fsm.Tick();
            EvaluateTransitions();
        }

        private void EvaluateTransitions()
        {
            var agentPosition = _controller.Position;
            var targetPosition = _navigationController.FindNearestEntity(agentPosition);

            if (targetPosition == null)
            {
                RequestStateChange("Idle");
                return;
            }

            _targetPosition = targetPosition.Value.Position;

            switch (_fsm.CurrentState)
            {
                case IdleState when targetPosition.HasValue:
                    RequestStateChange("Move");
                    break;
            }
        }

        public void Punch(Position3 velocity)
        {
            _controller.Punch(velocity);
        }

        public void Die(Position3 velocity)
        {
            _controller.Die(velocity);
        }

        public void StartMovement()
        {
            _controller.StartMovement();
        }

        public void MoveTowardsTarget()
        {
            _controller.SetDestination(_targetPosition);
        }

        public void StopMovement()
        {
            _controller.StopMovement();
        }

        private void RequestStateChange(string action)
        {
            _fsm.ChangeState(action);
        }
    }
}