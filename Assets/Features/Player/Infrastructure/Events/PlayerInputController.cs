using Core.PlayerInput;
using Feature.Player.Application;
using Shared.Data;
using System;
using Features.Core.Infrastructure.Input.Inputs.Interfaces;
using Zenject;

namespace Feature.Player.Infrastructure
{
    public class PlayerInputController : IInitializable, IDisposable
    {
        private readonly MovementInputState _movementState;
        private readonly MovementUseCase _movementUseCase;
        private readonly IMovementInput _movementInput;

        public PlayerInputController(
            MovementInputState movementState,
            MovementUseCase movementUseCase,
            IMovementInput movementInput)
        {
            _movementState = movementState;
            _movementUseCase = movementUseCase;
            _movementInput = movementInput;
        }
        public void Initialize()
        {
            _movementInput.MoveChanged += OnMoveChanged;
            _movementInput.JumpPressed += OnJumpPressed;
            _movementInput.JumpReleased += OnJumpReleased;
            _movementInput.SprintPressed += OnSprintPressed;
            _movementInput.SprintReleased += OnSprintReleased;
        }

        private void OnMoveChanged(Position2 dir)
        {
            _movementState.InputDirection = dir;
            _movementUseCase.Move();
        }

        private void OnJumpPressed()
        {
            _movementState.IsJumping = true;
            _movementUseCase.Move();
        }

        private void OnJumpReleased()
        {
            _movementState.IsJumping = false;
            _movementUseCase.Move();
        }

        private void OnSprintPressed()
        {
            _movementState.IsSprinting = true;
            _movementUseCase.Move();
        }

        private void OnSprintReleased()
        {
            _movementState.IsSprinting = false;
            _movementUseCase.Move();
        }

        public void Dispose()
        {
            _movementInput.MoveChanged -= OnMoveChanged;
            _movementInput.JumpPressed -= OnJumpPressed;
            _movementInput.JumpReleased -=  OnJumpReleased;
            _movementInput.SprintPressed -= OnSprintPressed;
            _movementInput.SprintReleased -= OnSprintReleased;
        }

    }
}