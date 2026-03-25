using UnityEngine;

namespace Feature.PlayerCamera.Infrastructure
{
    public interface IPhysicsService
    {
        void ApplyPitch(float pitch);
        void ApplyFOV(float fov);
    }
}