using Feature.Agent.Application;

namespace Feature.Agent.Domain
{
    public class IdleState : IAgentState
    {
        private IAgentController agent;

        public IdleState(IAgentController agent)
        {
            this.agent = agent;
        }

        public void Enter()
        {
            // Set idle animation or behavior here
        }
        public void Execute() { }
        public void Exit() { }
    }
}