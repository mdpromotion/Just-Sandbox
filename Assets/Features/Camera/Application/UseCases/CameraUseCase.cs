using System;
using Core.Data;
using Feature.Storage.Domain;
using Features.Camera.Application.Helpers;
using Features.Camera.Domain;
using Shared.Providers;
using UnityEngine;

namespace Features.Camera.Application.UseCases
{
    /// <summary>
    /// Provides functionality for managing and updating the camera's rotation based on player input and game state.
    /// </summary>
    /// <remarks>This class relies on various services to calculate and apply camera rotations. It ensures
    /// that camera updates only occur when the player is controllable, and it handles potential errors during the
    /// application of rotations.</remarks>
    public class CameraUseCase
    {
        private const string LogTag = nameof(CameraUseCase);

        private readonly RotationCalculator _calculator;
        private readonly IReadOnlyCoreGameStates _coreState;
        private readonly CameraState _cameraState;
        private readonly IReadOnlyControlSettings _controlSettings;
        private readonly IPhysicsService _cameraPhysics;
        private readonly IPlayerTransformController _playerController;
        private readonly ILogger _logger;

        public CameraUseCase(
            RotationCalculator calculator,
            IReadOnlyCoreGameStates coreGameState,
            CameraState cameraState,
            IReadOnlyControlSettings controlSettings,
            IPhysicsService cameraPhysics,
            IPlayerTransformController playerController,
            ILogger logger)
        {
            _calculator = calculator;
            _coreState = coreGameState;
            _cameraState = cameraState;
            _controlSettings = controlSettings;
            _cameraPhysics = cameraPhysics;
            _playerController = playerController;
            _logger = logger;
        }

        public void CameraUpdate(Vector2 delta)
        {
            if (!_coreState.IsPlayerControllable) return;

            var (yaw, pitch) = _calculator.CalculateRotation(
                _cameraState.Yaw,
                _cameraState.Pitch,
                delta,
                _controlSettings.MouseSensitivity);

            ApplyCameraRotation(yaw, pitch);
        }

        private void ApplyCameraRotation(float yaw, float pitch)
        {
            _cameraState.Yaw = yaw;
            _cameraState.Pitch = pitch;

            try
            {
                _playerController.ApplyYaw(yaw);
                _cameraPhysics.ApplyPitch(pitch);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogTag, $"Failed to apply camera rotation: {ex.Message}");
            }
        }
    }
}