using Features.Agent.Application;
using Features.Agent.Application.Interfaces;
using Features.Agent.Domain.Interfaces;
using Features.Agent.Infrastructure.Assembler.Interfaces;

namespace Features.Agent.Infrastructure.Assembler
{
    public interface IFacadeFactory
    {
        AgentFacade Create(Feature.Agent.Domain.Agent agent, IAgentController controller);
    }

    public class FacadeFactory : IFacadeFactory
    {
        private readonly ILifeUseCase _lifeUseCase;
        private readonly IAgentLifeController _agentLifeController;


        public FacadeFactory(ILifeUseCase lifeUseCase, IAgentLifeController agentLifeController)
        {
            _lifeUseCase = lifeUseCase;
            _agentLifeController = agentLifeController;
        }

        public AgentFacade Create(Feature.Agent.Domain.Agent agent, IAgentController controller)
        {
            return new AgentFacade(agent, controller, _lifeUseCase, _agentLifeController);
        }
    }
}