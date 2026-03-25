using Core.Service;
using Feature.Agent.Application;
using Feature.Agent.Data;
using UnityEngine;

namespace Feature.Agent.Infrastructure
{
    public class AgentAssembler : IAgentAssembler
    {
        private readonly IAgentFactory _agentFactory;
        private readonly IAgentComponentResolver _agentComponentResolver;
        private readonly IAgentControllerFactory _controllerFactory;
        private readonly IAgentFsmFactory _fsmFactory;
        private readonly IFacadeFactory _facadeFactory;
        private readonly IWorldEntityService _entityService;

        public AgentAssembler(IAgentFactory agentFactory, 
            IAgentComponentResolver agentComponentResolver, 
            IAgentControllerFactory controllerFactory,
            IAgentFsmFactory fsmFactory, 
            IFacadeFactory facadeFactory, 
            IWorldEntityService entityService)
        {
            _agentFactory = agentFactory;
            _agentComponentResolver = agentComponentResolver;
            _controllerFactory = controllerFactory;
            _fsmFactory = fsmFactory;
            _facadeFactory = facadeFactory;
            _entityService = entityService;
        }

        public Result<FactoryOutput> CreateAgent(IAgentProvider provider, GameObject obj)
        {
            var agent = _agentFactory.Create(provider);

            var componentsResult = _agentComponentResolver.Resolve(obj);
            if (!componentsResult.IsSuccess)
                return Result<FactoryOutput>.Failure(componentsResult.Error);

            var components = componentsResult.Value;
            components.Controller.SetSpeed(provider.Speed);

            var fsm = _fsmFactory.CreateFSM();

            var controllerOutput = _controllerFactory.Create(agent, components.Controller, components.TriggerHandler, fsm);

            var facade = _facadeFactory.Create(agent, controllerOutput.Controller);

            _fsmFactory.InitFSM(fsm, controllerOutput.Controller);
            _entityService.Bind(agent, obj);

            return Result<FactoryOutput>.Success(
                new FactoryOutput(agent, fsm, facade, controllerOutput.Controller, controllerOutput.DamageController));
        }
    }
}