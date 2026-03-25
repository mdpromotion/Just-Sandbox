using Shared.Domain;
using System;

namespace Feature.Agent.Infrastructure
{
    public interface IAgentFactory
    {
        Domain.Agent Create(IAgentProvider provider);
    }

    public class AgentFactory : IAgentFactory
    {
        public Domain.Agent Create(IAgentProvider provider)
        {
            return new Domain.Agent(
                Guid.NewGuid(),
                Team.Enemy,
                provider.Name,
                provider.MaxHealth,
                provider.Speed,
                provider.Damage,
                provider.AttackSpeed,
                provider.VisionRange
            );
        }
    }
}