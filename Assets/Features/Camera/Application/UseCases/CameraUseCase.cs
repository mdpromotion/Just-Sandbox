using Core.Data;
using Feature.PlayerCamera.Domain;
using Feature.PlayerCamera.Infrastructure;
using Feature.Storage.Domain;
using Shared.Providers;
using System;
using UnityEngine;

namespace Feature.PlayerCamera.Application
{
    /// <summary>
    /// Provides functionality for managing and updating the camera's rotation based on player input and game state.
    /// </summary>
    /// <remarks>This class relies on various services to calculate and apply camera rotations. It ensures
    /// that camera updates only occur when the player is controllable, and it handles potential errors during the
    /// application of rotations.</remarks>
    public class CameraUseCase
    {
        private static readonly string LogTag = nameof(CameraUseCase);

        private readonly RotationCalculator _calculator;
        private readonly IReadOnlyCoreGameStates _coreState;
        private readonly CameraState _cameraState;
        private readonly IReadOnlyControlSettings _controlSettings;
        private readonly IPhysicsService _cameraPhysics;
        private readonly IPlayerTransformController _playerController;
        private readonly ILogger _logger;

        public CameraUseCase(
            RotationCalculator calculator,
            IReadOnlyCoreGameStates coregameState,
            CameraState cameraState,
            IReadOnlyControlSettings controlSettings,
            IPhysicsService cameraPhysics,
            IPlayerTransformController playerController,
            ILogger logger)
        {
            _calculator = calculator;
            _coreState = coregameState;
            _cameraState = cameraState;
            _controlSettings = controlSettings;
            _cameraPhysics = cameraPhysics;
            _playerController = playerController;
            _logger = logger;
        }

        public void CameraUpdate(Vector2 delta)
        {
            if (!_coreState.IsPlayerControllable) return;

            (float yaw, float pitch) = _calculator.CalculateRotation(
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