using Feature.Agent.Application;
using Feature.Core.Infrastructure;
using Shared.Domain;
using UnityEngine;

namespace Feature.Agent.Infrastructure
{
    public class AgentConfigurator : IAgentConfigurator
    {
        public Result Configure(GameObject go, Domain.Agent agent)
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