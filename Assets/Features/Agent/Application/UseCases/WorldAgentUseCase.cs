using Feature.Agent.Infrastructure;
using Feature.Toolbox.Infrastructure;
using Shared.Data;
using Shared.Domain;
using System;
using UnityEngine;

namespace Feature.Agent.Application
{
    /// <summary>
    /// Handles the spawning and initialization of non-player characters (NPCs) within the game world.
    /// </summary>
    /// <remarks>This class coordinates the configuration, creation, and linking of agents to game objects. It
    /// ensures that the necessary services are utilized to properly initialize and register NPCs within the game
    /// environment.</remarks>
    public class WorldAgentUseCase : IWorldAgentUseCase
    {
        private readonly IAgentConfigurator _configurator;
        private readonly IAgentAssembler _agentFactory;
        private readonly IAIUpdateService _aiUpdateService;

        public WorldAgentUseCase(
            IAgentConfigurator configurator,
            IAgentAssembler agentFactory,
            IAIUpdateService aiUpdateService)
        {
            _configurator = configurator;
            _agentFactory = agentFactory;
            _aiUpdateService = aiUpdateService;
        }

        public Result<AgentContext> SpawnNPC(IAgentProvider agentProvider, GameObject obj)
        {
            var factoryResult = _agentFactory.CreateAgent(agentProvider, obj);
            if (!factoryResult.IsSuccess)
                return Result<AgentContext>.Failure(factoryResult.Error);

            var agent = factoryResult.Value.Agent;
            var controller = factoryResult.Value.Controller;
            var damageController = factoryResult.Value.DamageController;

            var configResult = _configurator.Configure(obj, agent);
            if (!configResult.IsSuccess)
                return Result<AgentContext>.Failure(configResult.Error);

            _aiUpdateService.RegisterAgent(agent.Id, controller, damageController);

            var context = new AgentContext(obj, agentProvider.Id, agent.Id);

            return Result<AgentContext>.Success(context);
        }
        public Result DespawnNPC(Guid agentId)
        {
            _aiUpdateService.UnregisterAgent(agentId);
            return Result.Success();
        }
    }
}