using Shared.Data;
using System;

namespace Feature.Player.Domain
{
    public interface ILifeEvents
    {
        event Action<AttackInfo> ReceivedDamage;
        event Action PlayerDied;
        event Action PlayerRespawned;
    }
}