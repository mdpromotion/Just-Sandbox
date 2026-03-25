using Shared.Data;

namespace Feature.Agent.Application
{
    /// <summary>
    /// Provides a unified interface for managing agent interactions, including handling events related to agent damage
    /// and death.
    /// </summary>
    /// <remarks>AgentFacade subscribes to the agent's damage and death events and delegates their handling to
    /// the specified life use case. To function correctly, it requires an instance of the agent, a controller, and a
    /// life use case. This class is intended to simplify agent event management and encapsulate related logic for
    /// consumers.</remarks>
    public class AgentFacade
    {
        private readonly Domain.Agent _agent;
        private readonly IAgentController _controller;
        private readonly ILifeUseCase _lifeUseCase;
        private readonly IAgentLifeController _lifeController;

        public AgentFacade(
            Domain.Agent agent, 
            IAgentController controller, 
            ILifeUseCase lifeUseCase, 
            IAgentLifeController lifeController)
        {
            _agent = agent;
            _controller = controller;
            _lifeUseCase = lifeUseCase;
            _lifeController = lifeController;
            Subscribe();
        }

        private void Subscribe()
        {
            _agent.AgentDamaged += OnAgentDamaged;
            _agent.AgentDied += OnAgentDied;
        }

        private void OnAgentDamaged(AttackInfo attackInfo)
        {
            _lifeUseCase.OnAgentDamaged(_controller, attackInfo);
        }

        private void OnAgentDied(AttackInfo attackInfo)
        {
            _lifeUseCase.OnAgentDied(_controller, attackInfo);
            _lifeController.RequestBurial(_agent.Id);
        }
    }
}