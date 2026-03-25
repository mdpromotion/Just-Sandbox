#nullable enable
using Feature.Inventory.Infrastructure;
using Feature.Toolbox.Infrastructure;
using Shared.Data;
using Shared.Service;
using System;
using static Parser;

namespace Feature.Inventory.Application
{
    public class InteractionUseCase : IInteractionUseCase
    {
        private readonly IItemMover _itemMover;
        private readonly IInteractionWorldService _interactionWorldService;
        private readonly ITransformWorldService _worldService;
        private readonly IGameObjectProvider _objectProvider;
        private readonly IItemConfigService _itemConfig;

        public InteractionUseCase(
            IItemMover itemMover,
            IInteractionWorldService interactionWorldService,
            ITransformWorldService worldService,
            IGameObjectProvider objectProvider,
            IItemConfigService itemConfig)
        {
            _itemMover = itemMover;
            _interactionWorldService = interactionWorldService;
            _worldService = worldService;
            _objectProvider = objectProvider;
            _itemConfig = itemConfig;
        }

        /// <summary>
        /// Attempts to retrieve the current item from the interaction world.
        /// </summary>
        /// <remarks>The returned result indicates success if an item is available in the interaction world. If no
        /// item is found, the result will contain an error message describing the failure.</remarks>
        /// <returns>A <see cref="Result{ItemContext}"/> containing the item context if found; otherwise, a failure result with an
        /// error message.</returns>
        public Result<ItemContext> TryGetItemFromWorld()
        {
            ItemContext? itemContext = _interactionWorldService.TryGetItem();
            if (itemContext == null)
                return Result<ItemContext>.Failure("Failed to load item: object not found.");

            return Result<ItemContext>.Success(itemContext.Value);
        }

        /// <summary>
        /// Attempts to pick up the item associated with the specified world identifier and attach it to the inventory.
        /// </summary>
        /// <remarks>If the specified item cannot be found or an error occurs while attaching it to the inventory,
        /// the returned result will contain a failure message describing the issue.</remarks>
        /// <param name="currentWorldId">The unique identifier of the game world object to pick up. Must correspond to an existing item in the current
        /// world.</param>
        /// <returns>A <see cref="Result"/> indicating whether the item was successfully picked up and attached to the inventory.
        /// Returns a failure result if the item is not found or if an error occurs during pickup.</returns>
        public Result TryPickupItem(Guid currentWorldId, int configId)
        {
            var obj = _objectProvider.Get(currentWorldId);
            if (obj == null)
                return Result.Failure("Failed to pick up item: game object not found.");

            var itemConfigResult = _itemConfig.GetItemConfig(configId);
            if (!itemConfigResult.IsSuccess || itemConfigResult == null)
                return Result.Failure($"Failed to pick up item: item configuration not found: {itemConfigResult?.Error}");

            var itemConfig = itemConfigResult.Value;

            try
            {
                _itemMover.PickupItem(obj, itemConfig?.DisplayOffset);
            }
            catch (Exception ex)
            {
                return Result.Failure($"Failed to equip item: {ex.Message}");
            }

            return Result.Success();
        }

        /// <summary>
        /// Attempts to update the selection of an item in the world based on the provided snapshot, attaching the relevant
        /// objects to their target parents as appropriate.
        /// </summary>
        /// <remarks>If the snapshot indicates an item is present in the slot, the corresponding object is
        /// attached to the hand; otherwise, the previous object is attached to the inventory. This method does not throw
        /// exceptions for missing objects or identifiers.</remarks>
        /// <param name="snapshot">A snapshot representing the current and previous selection state, including world identifiers and slot
        /// information. Cannot be null.</param>
        /// <returns>A Result indicating whether the selection update was processed successfully.</returns>
        public Result TrySelectItem(Guid? previousWorldId, Guid? currentWorldId, bool hasItemInSlot)
        {
            if (TryGetValue(previousWorldId, out var prevWorldId))
            {
                var previousObj = _objectProvider.Get(prevWorldId);
                if (previousObj != null)
                    _itemMover.MoveToInventory(previousObj);
            }

            if (hasItemInSlot)
            {
                if (TryGetValue(currentWorldId, out var curWorldId))
                {
                    var currentObj = _objectProvider.Get(curWorldId);
                    if (currentObj != null)
                        _itemMover.MoveToHand(currentObj);
                }
            }

            return Result.Success();
        }

        /// <summary>
        /// Attempts to drop an item in the specified world and returns the result of the operation.
        /// </summary>
        /// <param name="currentWorldId">The unique identifier of the world in which to drop the item. Must correspond to an existing game object.</param>
        /// <returns>A <see cref="Result"/> indicating whether the item was successfully dropped. Returns a failure result if the
        /// game object is not found, the drop direction cannot be determined, or an error occurs during the drop operation.</returns>
        public Result TryDropItem(Guid currentWorldId)
        {
            var obj = _objectProvider.Get(currentWorldId);
            if (obj == null)
                return Result.Failure($"Failed to drop item: game object not found.");

            var dropResult = _worldService.GetDropDirection();
            if (!dropResult.IsSuccess)
                return Result.Failure(dropResult.Error!);

            var dropValue = dropResult.Value;

            try
            {
                _itemMover.DropItem(obj, dropValue.Position, dropValue.Rotation);
            }
            catch (Exception ex)
            {
                return Result.Failure($"Failed to drop item: {ex.Message}");
            }

            return Result.Success();
        }

    }
}