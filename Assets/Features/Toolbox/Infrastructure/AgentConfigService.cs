using Feature.Agent.Infrastructure;

namespace Feature.Toolbox.Infrastructure
{
    public class AgentConfigService : IAgentConfigService
    {
        private readonly AgentDatabase _agentDatabase;

        public AgentConfigService(AgentDatabase agentDatabase) 
        { 
            _agentDatabase = agentDatabase; 
        }

        public Result<IAgentProvider> GetAgentConfig(int id)
        {
            var item = _agentDatabase.GetById(id);
            if (item == null) 
            {
                return Result<IAgentProvider>.Failure($"No agent found with ID: {id}");
            }

            return Result<IAgentProvider>.Success(new AgentProvider(item));
        }
    }
}