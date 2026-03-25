using Shared.Data;
using Shared.Domain;
using System;

namespace Feature.Agent.Domain
{
    public class Agent : IEntity, ITarget, ILifeEvents
    {
        public Guid Id { get; private set; }
        public Team Team { get; private set; }
        public string Name { get; private set; }
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public bool IsAlive => CurrentHealth > 0;

        public float Speed { get; private set; }
        public float Damage { get; private set; }
        public float AttackSpeed { get; private set; }
        public float VisionRange { get; private set; }

        public event Action<AttackInfo> AgentDamaged;
        public event Action<AttackInfo> AgentDied;

        public Agent(
            Guid id,
            Team team,
            string name,
            float maxHealth,
            float speed,
            float damage,
            float attackSpeed,
            float visionRange)
        {
            Id = id;
            Team = team;
            Name = name;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            Speed = speed;
            Damage = damage;
            AttackSpeed = attackSpeed;
            VisionRange = visionRange;
        }

        public Result ReceiveDamage(AttackInfo attackInfo)
        {
            if (!IsAlive) return Result.Failure($"Agent {Name} is died.");

            CurrentHealth -= attackInfo.Damage;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                AgentDied?.Invoke(attackInfo);
            }
            else
            {
                AgentDamaged?.Invoke(attackInfo);
            }

            return Result.Success();
        }
    }
}