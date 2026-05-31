using Core.Data;
using Feature.Agent.Application;
using Feature.Agent.Domain;
using Features.Agent.Application.Controllers.Interfaces;
using Features.Agent.Domain.Interfaces;
using Shared.Data;

namespace Features.Agent.Application.Controllers
{
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
            var targetFound = _navigationController.FindNearestEntity(agentPosition, out var nearestEntity);

            if (!targetFound)
            {
                RequestStateChange("Idle");
                return;
            }

            _targetPosition = nearestEntity.Position;

            switch (_fsm.CurrentState)
            {
                case IdleState when true:
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