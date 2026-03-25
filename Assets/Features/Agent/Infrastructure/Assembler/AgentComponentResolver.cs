using UnityEngine;

namespace Feature.Agent.Infrastructure
{
    public interface IAgentComponentResolver
    {
        Result<ResolverOutput> Resolve(GameObject obj);
    }

    public class AgentComponentResolver : IAgentComponentResolver
    {
        public Result<ResolverOutput> Resolve(GameObject obj)
        {
            if (!obj.TryGetComponent<NavMeshController>(out var nav))
                return Result<ResolverOutput>
                    .Failure($"No NavMeshController on {obj.name}");

            var trigger = obj.GetComponentInChildren<TriggerHandler>();
            if (trigger == null)
                return Result<ResolverOutput>
                    .Failure($"No TriggerHandler on {obj.name}");

            return Result<ResolverOutput>.Success(new ResolverOutput(nav, trigger));
        }
    }
}