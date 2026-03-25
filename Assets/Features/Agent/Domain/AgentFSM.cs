using System.Collections.Generic;

namespace Feature.Agent.Domain
{
    public class AgentFSM
    {
        public IAgentState CurrentState { get; private set; }
        private readonly Dictionary<string, IAgentState> _states = new();

        public void RegisterState(string name, IAgentState state)
        {
            _states[name] = state;
        }

        public void ChangeState(string name)
        {
            if (_states.TryGetValue(name, out var newState))
            {
                CurrentState?.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
        }

        public void Tick()
        {
            CurrentState?.Execute();
        }
    }
}