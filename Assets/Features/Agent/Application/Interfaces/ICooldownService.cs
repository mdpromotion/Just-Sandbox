using System;

namespace Feature.Agent.Domain
{
    public interface ICooldownService
    {
        bool CanAttack(Guid id, float now);
        void UpdateAttackTime(Guid id, float now, float cooldown);
    }
}