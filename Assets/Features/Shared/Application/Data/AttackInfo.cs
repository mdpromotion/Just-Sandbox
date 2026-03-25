namespace Shared.Data
{
    public readonly struct AttackInfo
    {
        public float Damage { get; }
        public float Knockback { get; }
        public Position3 AttackerPosition { get; }

        public AttackInfo(float damage, float knockback, Position3 attackerPosition)
        {
            Damage = damage;
            Knockback = knockback;
            AttackerPosition = attackerPosition;
        }
    }
}