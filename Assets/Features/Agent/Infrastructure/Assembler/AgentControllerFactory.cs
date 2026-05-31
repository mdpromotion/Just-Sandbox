using Core.Data;
using Core.Service;
using Feature.Agent.Application;
using Feature.Agent.Domain;
using Feature.Agent.Infrastructure;
using Features.Agent.Application.Controllers;
using Features.Agent.Application.Controllers.Interfaces;
using Features.Agent.Infrastructure.Assembler.Data;
using Features.Agent.Infrastructure.Assembler.Interfaces;
using Features.Agent.Infrastructure.Controllers;
using Features.Core.Interfaces;

namespace Features.Agent.Infrastructure.Assembler
{
    public interface IAgentControllerFactory
    {
        AgentControllerOutput Create(Feature.Agent.Domain.Agent agent, INavMeshController navMesh,
            TriggerHandler trigger, AgentFSM fsm);
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

        public AgentControllerOutput Create(Feature.Agent.Domain.Agent agent, INavMeshController navMesh,
            TriggerHandler trigger, AgentFSM fsm)
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