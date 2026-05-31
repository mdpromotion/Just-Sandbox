namespace Features.Agent.Domain.Interfaces
{
    public interface IAgentState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}