using Feature.Combat.Application;
using Feature.Inventory.Application;
using Feature.Items.Application;
using System.Threading.Tasks;
using UnityEngine;

namespace Feature.Toolbox.Application
{
    /// <summary>
    /// Coordinates the spawning of items from the toolbox, managing their creation and placement in the world or
    /// inventory based on the current toolbox state and user input.
    /// </summary>
    /// <remarks>This orchestrator integrates multiple item-related use cases, including world item spawning,
    /// weapon item handling, and inventory pickup. It ensures that items are spawned correctly and logs any errors
    /// encountered during the process. Use this class when you need to centralize item spawning logic and handle
    /// interactions between toolbox, world, and inventory systems.</remarks>
    public class SpawnItemFromToolboxOrchestrator : ISpawnUseCase
    {
        public static readonly string LogTag = nameof(SpawnItemFromToolboxOrchestrator);

        private readonly IItemSpawnUseCase _toolboxSpawnUseCase;
        private readonly IWorldItemUseCase _worldItemUseCase;
        private readonly IWeaponItemUseCase _weaponItemUseCase;
        private readonly IInventoryPickupInput _inventoryPickupInput;
        private readonly IReadOnlyToolboxState _toolboxState;
        private readonly ILogger _logger;

        public SpawnItemFromToolboxOrchestrator(
            IItemSpawnUseCase toolboxSpawnUseCase,
            IWorldItemUseCase worldItemUseCase,
            IWeaponItemUseCase weaponItemUseCase,
            IInventoryPickupInput inventoryPickupInput,
            IReadOnlyToolboxState toolboxState,
            ILogger logger)
        {
            _toolboxSpawnUseCase = toolboxSpawnUseCase;
            _worldItemUseCase = worldItemUseCase;
            _weaponItemUseCase = weaponItemUseCase;
            _inventoryPickupInput = inventoryPickupInput;
            _toolboxState = toolboxState;
            _logger = logger;
        }

        public async Task TrySpawn(int id)
        {
            int textureId = _toolboxState.TextureID;

            var toolboxResult = await _toolboxSpawnUseCase.TrySpawnObject(id, textureId);

            if (!toolboxResult.IsSuccess)
            {
                _logger.LogWarning(LogTag, toolboxResult.Error);
                return;
            }

            var toolboxData = toolboxResult.Value;

            var worldItemResult = _worldItemUseCase.SpawnItem(toolboxData.Config, toolboxData.Object, toolboxData.Material);

            if (!worldItemResult.IsSuccess)
            {
                _logger.LogError(LogTag, worldItemResult.Error);
                return;
            }

            if (toolboxData.Config is IWeaponProvider config)
            {
                _weaponItemUseCase.SpawnWeapon(config, worldItemResult.Value.WorldId);
            }

            if (_toolboxState.SpawnToInventory)
            {
                var result = _inventoryPickupInput.TryPickupSpawnedItem(worldItemResult.Value);
                if (!result.IsSuccess)
                {
                    _logger.LogError(LogTag, result.Error);
                }
                _inventoryPickupInput.TrySelectItem(result.Value);
            }
        }
    }
}