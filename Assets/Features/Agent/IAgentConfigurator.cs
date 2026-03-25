using UnityEngine;

namespace Feature.Agent.Infrastructure
{
    public interface IAgentConfigurator
    {
        Result Configure(GameObject go, Domain.Agent agent);
    }
}