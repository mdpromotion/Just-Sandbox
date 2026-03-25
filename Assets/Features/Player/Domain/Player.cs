using Shared.Data;
using Shared.Domain;
using System;

namespace Feature.Player.Domain
{
    public sealed class Player : IEntity, ITarget, IRespawnable, ILifeEvents, IReadOnlyPlayer
    {
        public Guid Id { get; private set; }
        public Team Team { get; private set; }
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public bool IsAlive { get; private set; }

        public event Action<AttackInfo> ReceivedDamage;
        public event Action PlayerDied;
        public event Action PlayerRespawned;

        public Player(
            float maxHealth = 100f,
            float jumpHeight = 2.5f,
            Team team = Team.Neutral)
        {
            Id = Guid.NewGuid();
            SetMaxHealth(maxHealth);
            SetHealth(maxHealth);
            Team = team;

            if (CurrentHealth > 0)
                IsAlive = true;
        }

        public Result ReceiveDamage(AttackInfo attackInfo)
        {
            if (!IsAlive)
                return Result.Failure("Player is already died");

            CurrentHealth -= attackInfo.Damage;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
                Die();
            }

            ReceivedDamage?.Invoke(attackInfo);

            return Result.Success();
        }

        public Result Die()
        {
            if (CurrentHealth > 0)
                return Result.Failure("Player cannot die with health above zero.");

            if (!IsAlive)
                return Result.Failure("Player is already dead.");

            IsAlive = false;
            PlayerDied?.Invoke();
            return Result.Success();
        }

        public Result Respawn()
        {
            if (IsAlive)
                return Result.Failure("Player is not dead and cannot respawn.");

            IsAlive = true;
            SetHealth(MaxHealth);
            PlayerRespawned?.Invoke();
            return Result.Success();
        }

        private void SetHealth(float health)
        {
            if (health < 0)
                return;

            CurrentHealth = health;
            return;
        }

        private void SetMaxHealth(float maxHealth)
        {
            if (maxHealth < 0)
                return;

            MaxHealth = maxHealth;
            return;
        }
    }
}