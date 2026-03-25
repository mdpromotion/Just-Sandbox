namespace Feature.Agent.Infrastructure
{
    public interface IAgentConfigService
    {
        Result<IAgentProvider> GetAgentConfig(int id);
    }
}