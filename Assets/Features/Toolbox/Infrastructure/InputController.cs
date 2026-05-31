using Core.PlayerInput;
using Feature.Toolbox.Application;
using System;
using Features.Core.Infrastructure.Input.Inputs.Interfaces;
using Zenject;

namespace Feature.Toolbox.Infrastructure
{
    public class InputController : IInitializable, IDisposable
    {
        private readonly IInventoryInput _toolboxInput;
        private readonly IMenuUseCase _useCase;

        public InputController(IInventoryInput toolboxInput, IMenuUseCase useCase)
        {
            _toolboxInput = toolboxInput;
            _useCase = useCase;
        }

        public void Initialize()
        {
            _toolboxInput.ToolPressed += OnToolPressed;
        }
        private void OnToolPressed()
        {
            _useCase.ToggleToolbox();
        }

        public void Dispose()
        {
            _toolboxInput.ToolPressed -= OnToolPressed;
        }
    }
}