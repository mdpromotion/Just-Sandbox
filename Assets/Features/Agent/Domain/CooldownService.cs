using System;
using System.Collections.Generic;

namespace Feature.Agent.Domain
{
    public class CooldownService : ICooldownService
    {
        private readonly Dictionary<Guid, float> _availableAt = new();

        public bool CanAttack(Guid id, float now)
        {
            return now >= _availableAt.GetValueOrDefault(id);
        }

        public void UpdateAttackTime(Guid id, float now, float cooldown)
        {
            _availableAt[id] = now + cooldown;
        }
    }
}
