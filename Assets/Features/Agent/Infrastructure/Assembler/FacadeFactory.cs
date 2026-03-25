using Feature.Agent.Application;

namespace Feature.Agent.Infrastructure
{
    public interface IFacadeFactory
    {
        AgentFacade Create(Domain.Agent agent, IAgentController controller);
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

        public AgentFacade Create(Domain.Agent agent, IAgentController controller)
        {
            return new AgentFacade(agent, controller, _lifeUseCase, _agentLifeController);
        }
    }
}