using Shared.Data;
using Shared.Domain;

namespace Feature.Player.Data
{
    public readonly struct AttackData
    {
        public IEntity Attacker { get; }
        public float Damage { get; }
        public float AttackSpeed { get; }
        public ITarget Target { get; }
        public Position3 AttackerPosition { get; }

        public AttackData(IEntity attacker, float damage, float attackSpeed, ITarget target, Position3 attackerPosition)
        {
            Attacker = attacker;
            Damage = damage;
            AttackSpeed = attackSpeed;
            Target = target;
            AttackerPosition = attackerPosition;
        }
    }
}