using Feature.Agent.Application;
using Feature.Agent.Domain;

namespace Feature.Agent.Infrastructure
{
    public interface IAgentFsmFactory
    {
        AgentFSM CreateFSM();
        void InitFSM(AgentFSM fsm, AgentController controller);
    }

    public class AgentFsmFactory : IAgentFsmFactory
    {
        public AgentFSM CreateFSM()
        {
            return new AgentFSM();
        }
        public void InitFSM(AgentFSM fsm, AgentController controller)
        {
            fsm.RegisterState("Idle", new IdleState(controller));
            fsm.RegisterState("Move", new MoveState(controller));

            fsm.ChangeState("Idle");
        }
    }
}