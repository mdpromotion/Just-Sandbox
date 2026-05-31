using Feature.Agent.Infrastructure;
using Feature.Core.Infrastructure;
using UnityEngine;

namespace Features.Agent.Infrastructure.Services
{
    public class AgentConfigurator : IAgentConfigurator
    {
        public Result Configure(GameObject go, Feature.Agent.Domain.Agent agent)
        {
            if (go.TryGetComponent(out EntityWorldBind view))
            {
                view.Bind(agent, agent);
                return Result.Success();
            }
            else
            {
                return Result.Failure($"GameObject {go.name} does not have a WorldAgentBind component.");
            }
        }
    }
}