using Zenject;

namespace Core.PlayerInput
{
    public class InputManager : ITickable
    {
        private readonly IMovementInput MovementInput;
        private readonly IInventoryInput InventoryInput;
        private readonly ICameraInput CameraInput;
        private readonly IInteractionInput InteractionInput;
        private readonly ISpecialButtonInput SpecialInput;


        public InputManager(
            IMovementInput movementInput,
            IInventoryInput inventoryInput,
            ICameraInput cameraInput,
            IInteractionInput interactionInput,
            ISpecialButtonInput specialInput)
        {
            MovementInput = movementInput;
            InventoryInput = inventoryInput;
            CameraInput = cameraInput;
            InteractionInput = interactionInput;
            SpecialInput = specialInput;
        }

        public void Tick()
        {
            MovementInput.Tick();
            InventoryInput.Tick();
            CameraInput.Tick();
            InteractionInput.Tick();
            SpecialInput.Tick();
        }
    }
}