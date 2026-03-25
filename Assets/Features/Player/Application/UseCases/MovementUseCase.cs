using Core.Data;
using Shared.Data;
using Shared.Providers;
using System;
using UnityEngine;

namespace Feature.Player.Application
{
    /// <summary>
    /// Provides functionality for handling player movement by updating the player's position and movement state based
    /// on current input and game conditions.
    /// </summary>
    /// <remarks>Movement operations are performed only when the player is controllable. The class supports
    /// both ground and flight movement modes, calculating the appropriate velocity for each. Errors encountered during
    /// movement are logged for diagnostic purposes but do not interrupt gameplay. Use this class to encapsulate player
    /// movement logic within the game loop or input handling routines.</remarks>
    public class MovementUseCase
    {
        private static readonly string LogTag = nameof(MovementUseCase);

        private readonly IMovementCalculator _calculator;
        private readonly IReadOnlyCoreGameStates _coreState;

        private readonly IPlayerTransformData _transformProvider;
        private readonly IPhysicsController _playerController;
        private readonly IReadOnlyMovementInputState _movementState;
        private readonly IReadOnlyPlayerWorldState _playerState;

        private readonly ILogger _logger;

        private readonly float _playerSpeed = 5f;
        private readonly float _jumpHeight = 2f;

        public MovementUseCase(
            IMovementCalculator calculator,
            IReadOnlyCoreGameStates coreState,
            IPlayerTransformData transformProvider,
            IPhysicsController playerController,
            IReadOnlyMovementInputState movementState,
            IReadOnlyPlayerWorldState playerState,
            ILogger logger)
        {
            _calculator = calculator;
            _coreState = coreState;
            _transformProvider = transformProvider;
            _playerController = playerController;
            _movementState = movementState;
            _playerState = playerState;
            _logger = logger;
        }

        /// <summary>
        /// Updates the player's position and movement state based on the specified movement input.
        /// </summary>
        /// <remarks>Movement is only performed if the player is available. If the player is flying, flight
        /// velocity is calculated; otherwise, ground velocity is used. If movement cannot be performed, a warning is
        /// logged. Errors during physics movement are logged but not propagated.</remarks>
        /// <param name="state">An object containing the current movement input and state, such as direction, jump, and sprint flags. Cannot be
        /// null.</param>
        public void Move()
        {
            if (_coreState.Game.IsPaused)
            {
                _playerController.SwitchKinematicState(true);
                return;
            }

            if (!_coreState.IsPlayerControllable)
                return;

            bool isJumping = _movementState.IsJumping && _playerState.IsGrounded;
            Position3 velocity = GetVelocity(isJumping);

            _playerController.SwitchKinematicState(false);
            var result = _playerController?.Move(velocity);
            if (!result.IsSuccess)
            {
                _logger.LogWarning(LogTag, $"Failed to move player: {result.Error}");
                return;
            }
        }

        private Position3 GetVelocity(bool isJumping)
        {
            var currentVelocity = _playerController.CurrentVelocity;

            Position3 velocity;

            velocity = _calculator.CalculateGroundVelocity(
                _movementState.InputDirection,
                _transformProvider,
                _playerSpeed,
                _jumpHeight,
                currentVelocity.Y,
                _movementState.IsSprinting,
                isJumping);

            return velocity;
        }
    }
}