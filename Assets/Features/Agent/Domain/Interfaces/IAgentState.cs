namespace Feature.Agent.Domain
{
    public interface IAgentState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}