using Core.Data;
using Core.Service;
using Feature.Agent.Domain;
using Feature.Agent.Infrastructure;

namespace Feature.Agent.Application
{
    public interface IAgentControllerFactory
    {
        AgentControllerOutput Create(Domain.Agent agent, NavMeshController navMesh, TriggerHandler trigger, AgentFSM fsm);
    }

    public class AgentControllerFactory : IAgentControllerFactory
    {
        private readonly IWorldEntityService _entityService;
        private readonly IReadOnlyCoreGameStates _gameState;
        private readonly IAttackUseCase _attackUseCase;

        public AgentControllerFactory(IWorldEntityService entityService, IReadOnlyCoreGameStates gameState, IAttackUseCase attackUseCase)
        {
            _entityService = entityService;
            _gameState = gameState;
            _attackUseCase = attackUseCase;
        }

        public AgentControllerOutput Create(Domain.Agent agent, NavMeshController navMesh, TriggerHandler trigger, AgentFSM fsm)
        {
            var navigationController = new NavigationController(_entityService, agent, agent.VisionRange);

            var agentController = new AgentController(
                navigationController,
                _gameState,
                navMesh,
                fsm
            );

            var damageController = new DamageController(agent, _attackUseCase, trigger, navMesh);

            return new AgentControllerOutput(agentController, damageController);
        }
    }
}