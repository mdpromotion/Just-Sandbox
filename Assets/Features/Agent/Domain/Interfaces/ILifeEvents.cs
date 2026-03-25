using Shared.Data;
using System;

namespace Feature.Agent.Domain
{
    public interface ILifeEvents
    {
        event Action<AttackInfo> AgentDamaged;
        event Action<AttackInfo> AgentDied;
    }
}