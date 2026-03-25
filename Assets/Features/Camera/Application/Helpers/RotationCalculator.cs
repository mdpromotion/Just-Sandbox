using Unity.Cinemachine;
using UnityEngine;

namespace Feature.PlayerCamera.Application
{
    /// <summary>
    /// Provides functionality to calculate yaw and pitch rotations based on input deltas, applying sensitivity and
    /// clamping pitch within specified limits.
    /// </summary>
    /// <remarks>Use this class to update camera or object rotation in response to user input, ensuring pitch
    /// remains within defined minimum and maximum bounds. The class is suitable for scenarios where controlled rotation
    /// is required, such as first-person camera movement or object orientation in 3D environments.</remarks>
    public class RotationCalculator
    {
        private readonly float _minPitch;
        private readonly float _maxPitch;

        public RotationCalculator(
            float minPitch = -90f,
            float maxPitch = 90f)
        {
            _minPitch = minPitch;
            _maxPitch = maxPitch;
        }

        public (float newYaw, float newPitch) CalculateRotation(
            float yaw,
            float pitch,
            Vector2 delta,
            float sensitivity)
        {
            float newYaw = yaw + delta.x * sensitivity;
            float newPitch = Clamp(pitch - delta.y * sensitivity);
            return (newYaw, newPitch);
        }

        private float Clamp(float value)
        {
            if (value < _minPitch) return _minPitch;
            if (value > _maxPitch) return _maxPitch;
            return value;
        }
    }
}
