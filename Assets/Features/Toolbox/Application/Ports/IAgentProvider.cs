
namespace Feature.Agent.Infrastructure
{
    public interface IAgentProvider
    {
        int Id { get; }
        string Name { get; }
        float MaxHealth { get; }
        float Speed { get; }
        float Damage { get; }
        float AttackSpeed { get; }
        float VisionRange { get; }
        string PrefabAddress { get; }
    }
}