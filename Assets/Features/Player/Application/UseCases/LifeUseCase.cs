using Core.Data;
using Shared.Data;
using Shared.Providers;
using System;
using UnityEngine;

namespace Feature.Player.Application
{
    /// <summary>
    /// Provides functionality for managing the player's life cycle, including handling damage, death, and respawn
    /// events.
    /// </summary>
    /// <remarks>This class coordinates with game state and player controllers to ensure that life-related
    /// events are processed correctly. It restricts damage reception to appropriate game states and manages player
    /// state transitions during death and respawn. Use this class to encapsulate player life logic and trigger related
    /// output or state changes as needed.</remarks>
    public class LifeUseCase : ILifeUseCase
    {
        private static readonly string LogTag = nameof(LifeUseCase);

        private readonly IPunchCalculator _calculator;
        private readonly IReadOnlyCoreGameStates _gameState;
        private readonly IPlayerTransformController _playerTransformController;
        private readonly IPlayerTransformData _transformData;
        private readonly ILogger _logger;

        public LifeUseCase(
            IPunchCalculator calculator,
            IReadOnlyCoreGameStates gameState,
            IPlayerTransformController playerTransformController,
            IPlayerTransformData transformData,
            ILogger logger)
        {
            _calculator = calculator;
            _gameState = gameState;
            _playerTransformController = playerTransformController;
            _transformData = transformData;
            _logger = logger;
        }


        public void OnPlayerDamaged(IPhysicsController controller, AttackInfo attackInfo)
        {
            if (!_gameState.IsPlayerControllable)
            {
                _logger.LogWarning(LogTag, "Player is not controllable and cannot receive damage.");
                return;
            }

            Position3 velocity = _calculator.CalculatePunchVelocity(
                attackInfo.AttackerPosition,
                _transformData.Position,
                attackInfo.Knockback);

            var result = controller.Punch(velocity);
            if (!result.IsSuccess)
            {
                _logger.LogWarning(LogTag, result.Error);
                return;
            }
        }
        public void OnPlayerDied(IPhysicsController controller)
        {
            controller.ToggleConstraints(false);
        }

        public void OnPlayerRespawned(IPhysicsController controller)
        {
            try
            {
                _playerTransformController.Teleport(Position3.Zero);
                _playerTransformController.ResetAngle();
                controller.SwitchKinematicState(false);
                controller.ToggleConstraints(true);
                controller.ResetVelocity();
            }
            catch (Exception ex)
            {
                _logger.LogError(LogTag, "Respawn failed");
                _logger.LogException(ex);
            }

        }

    }
}