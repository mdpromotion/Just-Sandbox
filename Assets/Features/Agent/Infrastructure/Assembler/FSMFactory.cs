using Feature.Agent.Domain;
using Features.Agent.Application.Controllers;

namespace Features.Agent.Infrastructure.Assembler
{
    public interface IAgentFsmFactory
    {
        AgentFSM CreateFsm();
        void InitFsm(AgentFSM fsm, AgentController controller);
    }

    public class AgentFsmFactory : IAgentFsmFactory
    {
        public AgentFSM CreateFsm()
        {
            return new AgentFSM();
        }
        public void InitFsm(AgentFSM fsm, AgentController controller)
        {
            fsm.RegisterState("Idle", new IdleState(controller));
            fsm.RegisterState("Move", new MoveState(controller));

            fsm.ChangeState("Idle");
        }
    }
}