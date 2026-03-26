using Feature.Agent.Application;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Feature.Toolbox.Application.UseCases
{
    /// <summary>
    /// Coordinates the spawning of non-player characters (NPCs) from a toolbox and their placement into the game world.
    /// </summary>
    /// <remarks>This orchestrator utilizes the NPCSpawnUseCase to attempt spawning an NPC from a toolbox and
    /// the IWorldAgentUseCase to place the NPC in the game world. It logs warnings if the spawning process fails at
    /// either stage. Use this class when you need to manage the full lifecycle of NPC spawning, including error
    /// handling and integration between toolbox and world systems.</remarks>
    public class SpawnNPCFromToolboxOrchestrator : ISpawnUseCase
    {
        public static readonly string LogTag = nameof(SpawnNPCFromToolboxOrchestrator);

        private readonly INPCSpawnUseCase _toolboxSpawnUseCase;
        private readonly IWorldAgentUseCase _worldAgentUseCase;
        private readonly ILogger _logger;

        public SpawnNPCFromToolboxOrchestrator(INPCSpawnUseCase toolboxSpawnUseCase, IWorldAgentUseCase worldAgentUseCase, ILogger logger)
        {
            _toolboxSpawnUseCase = toolboxSpawnUseCase;
            _worldAgentUseCase = worldAgentUseCase;
            _logger = logger;
        }

        public async UniTask TrySpawn(int id)
        {
            var toolboxResult = await _toolboxSpawnUseCase.TrySpawnNPC(id);
            if (!toolboxResult.IsSuccess)
            {
                _logger.LogWarning(LogTag, $"Failed to spawn NPC with id {id} from toolbox: {toolboxResult.Error}");
                return;
            }

            var toolboxData = toolboxResult.Value;

            var worldAgentResult = _worldAgentUseCase.SpawnNPC(toolboxData.Config, toolboxData.Object);
            if (!worldAgentResult.IsSuccess) 
            {
                _logger.LogWarning(LogTag, $"Failed to spawn NPC with id {id} in the world: {worldAgentResult.Error}");
                return;
            }
        }
    }
}