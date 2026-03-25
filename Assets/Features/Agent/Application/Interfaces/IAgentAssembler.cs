using Feature.Agent.Data;
using UnityEngine;

namespace Feature.Agent.Infrastructure
{
    public interface IAgentAssembler
    {
        Result<FactoryOutput> CreateAgent(IAgentProvider agentProvider, GameObject obj);
    }
}