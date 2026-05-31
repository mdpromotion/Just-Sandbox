using Core.Service;
using Feature.Agent.Infrastructure;
using Features.Agent.Application.Interfaces;
using Features.Agent.Data;
using UnityEngine;

namespace Features.Agent.Infrastructure.Assembler
{
    public class AgentAssembler : IAgentAssembler
    {
        private readonly IAgentFactory _agentFactory;
        private readonly IAgentComponentResolver _agentComponentResolver;
        private readonly IAgentControllerFactory _controllerFactory;
        private readonly IAgentFsmFactory _fsmFactory;
        private readonly IFacadeFactory _facadeFactory;
        private readonly IWorldEntityService _entityService;

        protected AgentAssembler(IAgentFactory agentFactory, 
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
                if (componentsResult.Error != null)
                    return Result<FactoryOutput>.Failure(componentsResult.Error);

            var components = componentsResult.Value;
            components.Controller.SetSpeed(provider.Speed);

            var fsm = _fsmFactory.CreateFsm();

            var controllerOutput = _controllerFactory.Create(agent, components.Controller, components.TriggerHandler, fsm);

            var facade = _facadeFactory.Create(agent, controllerOutput.Controller);

            _fsmFactory.InitFsm(fsm, controllerOutput.Controller);
            _entityService.Bind(agent, obj);

            return Result<FactoryOutput>.Success(
                new FactoryOutput(agent, fsm, facade, controllerOutput.Controller, controllerOutput.DamageController));
        }
    }
}