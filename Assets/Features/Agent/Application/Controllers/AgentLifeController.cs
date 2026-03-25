using Core.Service;
using Feature.Agent.Infrastructure;
using Feature.Factory.Infrastructure;
using Shared.Service;
using System;

namespace Feature.Agent.Application
{
    /// <summary>
    /// Manages the lifecycle of agents within the game, including their burial and unregistration from the AI system.
    /// </summary>
    /// <remarks>This controller interacts with various services to manage agent states and ensure proper
    /// cleanup when agents are no longer needed. It handles the unbinding of agents and their delayed release from
    /// memory after a specified period.</remarks>
    public class AgentLifeController : IAgentLifeController
    {
        private readonly IDelay _delayService;
        private readonly IWorldEntityService _entityService;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IAIUpdateService _aiService;

        public AgentLifeController(
            IDelay delayService, 
            IWorldEntityService entityService, 
            IGameObjectFactory gameObjectFactory, 
            IAIUpdateService aiService)
        {
            _delayService = delayService;
            _entityService = entityService;
            _gameObjectFactory = gameObjectFactory;
            _aiService = aiService;
        }

        public void RequestBurial(Guid entityId)
        {
            var agentObj = _entityService.GetGameObject(entityId);
            _entityService.Unbind(entityId);
            _aiService.UnregisterAgent(entityId);
            if (agentObj != null)
                _delayService.ExecuteAfterDelay(3f, () => _gameObjectFactory.Release(agentObj));
        }
    }
}
