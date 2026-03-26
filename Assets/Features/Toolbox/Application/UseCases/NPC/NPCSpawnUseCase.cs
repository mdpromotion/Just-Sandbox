using Cysharp.Threading.Tasks;
using Feature.Agent.Infrastructure;
using Feature.Factory.Infrastructure;
using Feature.Toolbox.Data;
using Shared.Service;

namespace Feature.Toolbox.Application
{
    /// <summary>
    /// Provides functionality to spawn non-player characters (NPCs) in the game environment based on agent
    /// configurations and available spawn points.
    /// </summary>
    /// <remarks>This class coordinates between configuration, world, and factory services to ensure NPCs are
    /// spawned only when valid configurations and spawn locations are available. It returns detailed results indicating
    /// success or failure for each spawn attempt, allowing callers to handle errors appropriately.</remarks>
    public class NPCSpawnUseCase : INPCSpawnUseCase
    {
        private readonly IGameObjectFactory _factory;
        private readonly ITransformWorldService _worldService;
        private readonly IAgentConfigService _configService;

        public NPCSpawnUseCase(IGameObjectFactory factory, ITransformWorldService worldService, IAgentConfigService configService)
        {
            _factory = factory;
            _worldService = worldService;
            _configService = configService;
        }

        public async UniTask<Result<AgentSpawnContext>> TrySpawnNPC(int id)
        {
            var configResult = _configService.GetAgentConfig(id);
            if (!configResult.IsSuccess)
                return Result<AgentSpawnContext>.Failure(configResult.Error);

            var spawnPointResult = _worldService.GetSpawnPoint();
            if (!spawnPointResult.IsSuccess)
                return Result<AgentSpawnContext>.Failure(spawnPointResult.Error);

            var config = configResult.Value;
            var spawnPoint = spawnPointResult.Value;

            var npc = await _factory.SpawnObject(
                config.PrefabAddress, 
                spawnPoint.Position,
                spawnPoint.Rotation);

            if (npc == null)
                return Result<AgentSpawnContext>.Failure(
                    $"Failed to spawn npc for item id {id}");

            return Result<AgentSpawnContext>.Success(new AgentSpawnContext(npc, config));
        }
    }
}