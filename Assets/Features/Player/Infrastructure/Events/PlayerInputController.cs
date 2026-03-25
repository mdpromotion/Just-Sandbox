using Core.PlayerInput;
using Feature.Player.Application;
using Shared.Data;
using System;
using Zenject;

namespace Feature.Player.Infrastructure
{
    public class PlayerInputController : IInitializable, IDisposable
    {
        private readonly MovementInputState _movementState;
        private readonly MovementUseCase _movementUseCase;
        private readonly IMovementInput _movementInput;

        private Action<Position2> _onMoveChanged;
        private Action _jumpPressed;
        private Action _jumpReleased;
        private Action _sprintPressed;
        private Action _sprintReleased;

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
            _onMoveChanged += dir => UpdateState(s => s.InputDirection = dir);
            _jumpPressed += () => UpdateState(s => s.IsJumping = true);
            _jumpReleased += () => UpdateState(s => s.IsJumping = false);
            _sprintPressed += () => UpdateState(s => s.IsSprinting = true);
            _sprintReleased += () => UpdateState(s => s.IsSprinting = false);

            _movementInput.MoveChanged += _onMoveChanged;
            _movementInput.JumpPressed += _jumpPressed;
            _movementInput.JumpReleased += _jumpReleased;
            _movementInput.SprintPressed += _sprintPressed;
            _movementInput.SprintReleased += _sprintReleased;
        }

        private void UpdateState(Action<MovementInputState> update)
        {
            update(_movementState);
            _movementUseCase.Move();
        }

        public void Dispose()
        {
            _movementInput.MoveChanged -= _onMoveChanged;
            _movementInput.JumpPressed -= _jumpPressed;
            _movementInput.JumpReleased -= _jumpReleased;
            _movementInput.SprintPressed -= _sprintPressed;
            _movementInput.SprintReleased -= _sprintReleased;
        }

    }
}