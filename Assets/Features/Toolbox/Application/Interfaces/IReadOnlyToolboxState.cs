using Shared.Domain;

public interface IReadOnlyToolboxState
{
    Team CurrentSpawnTeam { get; }
    bool IsAI { get; }
    bool SpawnToInventory { get; }
    int TextureID { get; }
}
