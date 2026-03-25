using Shared.Domain;

namespace Feature.Toolbox.Domain
{
    public interface IToolboxState
    {
        Team CurrentSpawnTeam { get; }
        bool SpawnToInventory { get; }
        bool ToggleToolbox();
        void SetSpawnTeam(Team team);
        void SetIsAI(bool isAi);
        void SetSpawnToInventory(bool spawnToInventory);
        Result SetTextureID(int id);
    }
}
