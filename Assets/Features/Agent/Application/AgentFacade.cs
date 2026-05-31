using Feature.Agent.Application;
using Features.Agent.Application.Interfaces;
using Features.Agent.Domain.Interfaces;
using Features.Agent.Infrastructure.Assembler.Interfaces;
using Shared.Data;

namespace Features.Agent.Application
{
    public class AgentFacade
    {
        private readonly Feature.Agent.Domain.Agent _agent;
        private readonly IAgentController _controller;
        private readonly ILifeUseCase _lifeUseCase;
        private readonly IAgentLifeController _lifeController;

        public AgentFacade(
            Feature.Agent.Domain.Agent agent, 
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