using Feature.Factory.Infrastructure;
using Feature.Toolbox.Data;
using Feature.Toolbox.Infrastructure;
using Shared.Service;
using System.Threading.Tasks;
using UnityEngine;

namespace Feature.Toolbox.Application
{
    /// <summary>
    /// Provides functionality for spawning game objects based on item and texture configurations within the game
    /// environment.
    /// </summary>
    /// <remarks>This class coordinates multiple services and factories to retrieve item configuration,
    /// determine spawn points, and apply materials. It ensures that all dependencies are satisfied before attempting to
    /// instantiate a game object. Use this class when you need to create game objects dynamically with specific item
    /// and texture settings.</remarks>
    public class ItemSpawnUseCase : IItemSpawnUseCase
    {
        private readonly IGameObjectFactory _factory;
        private readonly IMaterialFactory _materialFactory;
        private readonly ITransformWorldService _worldService;
        private readonly IItemConfigService _itemConfig;
        private readonly ITextureConfigService _textureConfig;

        public ItemSpawnUseCase(
            IGameObjectFactory factory,
            IMaterialFactory materialFactory,
            ITransformWorldService worldService,
            IItemConfigService itemConfig,
            ITextureConfigService textureConfig)
        {
            _factory = factory;
            _materialFactory = materialFactory;
            _worldService = worldService;
            _itemConfig = itemConfig;
            _textureConfig = textureConfig;
        }

        public async Task<Result<ItemSpawnContext>> TrySpawnObject(int objectId, int textureId)
        {
            var configResult = _itemConfig.GetItemConfig(objectId);
            if (!configResult.IsSuccess)
                return Result<ItemSpawnContext>.Failure(configResult.Error);

            var spawnPointResult = _worldService.GetSpawnPoint();
            if (!spawnPointResult.IsSuccess)
                return Result<ItemSpawnContext>.Failure(spawnPointResult.Error);

            var materialResult = await LoadMaterial(textureId);
            if (!materialResult.IsSuccess)
                return Result<ItemSpawnContext>.Failure(materialResult.Error);

            var config = configResult.Value;
            var spawnPoint = spawnPointResult.Value;
            var material = materialResult.Value;

            var obj = await _factory.SpawnObject(
                config.PrefabAddress,
                spawnPoint.Position,
                spawnPoint.Rotation,
                materialResult.Value);

            if (obj == null)
                return Result<ItemSpawnContext>.Failure(
                    $"Failed to spawn object for item id {objectId}");

            return Result<ItemSpawnContext>.Success(new ItemSpawnContext(obj, material, config));
        }

        private async Task<Result<Material>> LoadMaterial(int textureId)
        {
            var textureConfigResult = _textureConfig.GetMaterialAddress(textureId);
            if (!textureConfigResult.IsSuccess)
                return Result<Material>.Failure(textureConfigResult.Error);

            var material = await _materialFactory.GetMaterial(textureConfigResult.Value);
            if (material == null)
                return Result<Material>.Failure($"Failed to load material for texture id {textureId}");

            return Result<Material>.Success(material);
        }
    }
}