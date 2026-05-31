using UnityEngine;

namespace Features.Agent
{
    public interface IAgentConfigurator
    {
        Result Configure(GameObject go, Feature.Agent.Domain.Agent agent);
    }
}