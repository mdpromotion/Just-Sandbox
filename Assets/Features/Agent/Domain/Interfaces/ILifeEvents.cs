using System;
using Shared.Data;

namespace Features.Agent.Domain.Interfaces
{
    public interface ILifeEvents
    {
        event Action<AttackInfo> AgentDamaged;
        event Action<AttackInfo> AgentDied;
    }
}