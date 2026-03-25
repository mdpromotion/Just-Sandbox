using Shared.Domain;

namespace Feature.Toolbox.Domain
{

    /// <summary>
    /// Represents the state and configuration of the toolbox, including its visibility, assigned team, AI status,
    /// texture identifier, and inventory spawning behavior.
    /// </summary>
    /// <remarks>Use this class to manage and query the current toolbox state in scenarios where toolbox
    /// visibility, team assignment, AI control, texture selection, and inventory spawning need to be tracked or
    /// modified. The texture identifier must be a non-negative integer. Methods are provided to toggle the toolbox, set
    /// team and AI status, configure inventory spawning, and update the texture ID. This class is intended for use in
    /// environments where toolbox state management is required, such as gameplay or editor contexts.</remarks>
    public class ToolboxState : IToolboxState, IReadOnlyToolboxState
    {
        public bool IsToolboxOpen { get; private set; }
        public Team CurrentSpawnTeam { get; private set; }
        public bool IsAI { get; private set; }
        public bool SpawnToInventory { get; private set; }
        public int TextureID { get; private set; }

        public ToolboxState(
            bool isToolboxOpen = false,
            Team team = Team.Enemy,
            bool isAi = false,
            int textureId = 0,
            bool spawnToInventory = true)
        {
            IsToolboxOpen = isToolboxOpen;
            CurrentSpawnTeam = team;
            IsAI = isAi;
            TextureID = textureId;
            SpawnToInventory = spawnToInventory;
        }

        public bool ToggleToolbox() => IsToolboxOpen = !IsToolboxOpen;
        public void SetSpawnTeam(Team team) => CurrentSpawnTeam = team;
        public void SetIsAI(bool isAi) => IsAI = isAi;
        public void SetSpawnToInventory(bool spawnToInventory) => SpawnToInventory = spawnToInventory;
        public Result SetTextureID(int id)
        {
            if (id < 0)
                return Result.Failure("Texture ID cannot be negative.");

            TextureID = id;
            return Result.Success();
        }
    }
}