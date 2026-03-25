using Core.PlayerInput;
using Feature.Combat.Application;
using System;
using UnityEngine;
using Zenject;

namespace Feature.Combat.Infrastructure
{
    public class InputController : IInitializable, IDisposable
    {
        private readonly IUseItemInput _useItemUseCase;
        private readonly IInteractionInput _interactionInput;

        public InputController(
            IUseItemInput useItemUseCase,
            IInteractionInput interactionInput)
        {
            _useItemUseCase = useItemUseCase;
            _interactionInput = interactionInput;
        }

        public void Initialize()
        {
            _interactionInput.MouseHeld += OnUse;
            _interactionInput.ReloadPressed += OnReload;
        }

        void OnUse(MouseButton button)
        {
            if (button != MouseButton.Left)
                return;

            _useItemUseCase.Use();
        }
        void OnReload()
        {
            _useItemUseCase.Reload();
        }
        public void Dispose()
        {
            _interactionInput.MouseHeld -= OnUse;
            _interactionInput.ReloadPressed -= OnReload;
        }
    }
}