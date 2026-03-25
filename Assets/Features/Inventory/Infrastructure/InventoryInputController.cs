using Core.PlayerInput;
using Feature.Inventory.Application;
using System;
using Zenject;

namespace Feature.Inventory.Infrastructure
{
    public class InventoryInputController : IInitializable, IDisposable
    {
        private readonly IInventoryInput _inventoryInput;
        private readonly IInteractionInput _interactionInput;
        private readonly IInventoryPickupInput _inventoryPickup;

        public InventoryInputController(
            IInventoryInput inventoryInput,
            IInteractionInput interactionInput,
            IInventoryPickupInput inventoryPickup)
        {
            _inventoryInput = inventoryInput;
            _interactionInput = interactionInput;
            _inventoryPickup = inventoryPickup;
        }

        public void Initialize()
        {
            _inventoryInput.SlotPressed += OnSlotPressed;
            _interactionInput.InteractPressed += OnInteractPressed;
            _interactionInput.DropPressed += OnDropPressed;
        }

        public void Dispose()
        {
            _inventoryInput.SlotPressed -= OnSlotPressed;
            _interactionInput.InteractPressed -= OnInteractPressed;
            _interactionInput.DropPressed -= OnDropPressed;
        }

        private void OnSlotPressed(int? slotId)
        {
            if (!slotId.HasValue)
                return;

            _inventoryPickup.TrySelectItem(slotId.Value - 1); // Convert to zero-based index
        }
        private void OnInteractPressed()
        {
            _inventoryPickup.TryPickupItem();
        }
        private void OnDropPressed()
        {
            _inventoryPickup.TryDropItem();
        }
    }
}