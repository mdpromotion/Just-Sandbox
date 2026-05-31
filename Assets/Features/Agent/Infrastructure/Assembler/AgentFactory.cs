using System;
using Feature.Agent.Infrastructure;
using Shared.Domain;

namespace Features.Agent.Infrastructure.Assembler
{
    public interface IAgentFactory
    {
        Feature.Agent.Domain.Agent Create(IAgentProvider provider);
    }

    public class AgentFactory : IAgentFactory
    {
        public Feature.Agent.Domain.Agent Create(IAgentProvider provider)
        {
            return new Feature.Agent.Domain.Agent(
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