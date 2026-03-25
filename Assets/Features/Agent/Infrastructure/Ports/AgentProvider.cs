namespace Feature.Agent.Infrastructure
{
    public class AgentProvider : IAgentProvider
    {
        private readonly AgentData _agentData;

        public int Id => _agentData.Id;
        public string Name => _agentData.Name;
        public float MaxHealth => _agentData.MaxHealth;
        public float Speed => _agentData.Speed;
        public float Damage => _agentData.Damage;
        public float AttackSpeed => _agentData.AttackSpeed;
        public float VisionRange => _agentData.VisionRange;
        public string PrefabAddress => _agentData.PrefabAddress;

        public AgentProvider(AgentData agentData)
        {
            _agentData = agentData;
        }
    }
}