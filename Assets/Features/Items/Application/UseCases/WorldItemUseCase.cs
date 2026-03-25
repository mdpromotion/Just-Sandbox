using Feature.Items.Domain;
using Feature.Items.Infrastructure;
using Shared.Data;
using Shared.Repository;
using Shared.Service;
using System;
using UnityEngine;

namespace Feature.Items.Application
{
    /// <summary>
    /// Provides operations for spawning and managing world items within the game environment.
    /// </summary>
    /// <remarks>This class coordinates item configuration, linking, and repository management to ensure that
    /// world items are properly initialized and integrated with game objects. Use this type to add new items to the
    /// game world and handle their setup based on item type and context.</remarks>
    public class WorldItemUseCase : IWorldItemUseCase
    {
        private readonly IWorldItemRepository _worldItemRepository;
        private readonly IWorldItemConfigurator _configurator;
        private readonly IWorldItemLinkService _linkService;

        public WorldItemUseCase(
            IWorldItemRepository worldItemRepository,
            IWorldItemConfigurator configurator,
            IWorldItemLinkService linkService)
        {
            _worldItemRepository = worldItemRepository;
            _configurator = configurator;
            _linkService = linkService;
        }

        public Result<ItemContext> SpawnItem(IItemProvider item, GameObject obj, Material material = null)
        {
            var worldItem = new WorldItem(Guid.NewGuid(), item.Name, item.Id);

            var newMaterial = material;
            if (item is IWeaponProvider)
                newMaterial = null;

            var configResult = _configurator.Configure(obj, worldItem, newMaterial);
            if (!configResult.IsSuccess)
                return Result<ItemContext>.Failure(configResult.Error);

            _linkService.Bind(worldItem, obj);
            _worldItemRepository.Add(worldItem);
            return Result<ItemContext>.Success(new ItemContext(obj, item.Id, worldItem.Id));
        }
    }
}