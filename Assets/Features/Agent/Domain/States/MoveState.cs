using Feature.Agent.Application;

namespace Feature.Agent.Domain
{
    public class MoveState : IAgentState
    {
        private readonly IAgentController _agent;

        public MoveState(IAgentController agent)
        {
            _agent = agent;
        }

        public void Enter()
        {
            _agent.StartMovement();
        }
        public void Execute() 
        {
            _agent.MoveTowardsTarget();
        }
        public void Exit() 
        {
            _agent.StopMovement();
        }
    }
}