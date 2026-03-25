using Unity.Cinemachine;
using UnityEngine;

namespace Feature.PlayerCamera.Infrastructure
{
    public class PhysicsService : IPhysicsService
    {
        private readonly CinemachineCamera _camera;

        public PhysicsService(CinemachineCamera camera)
        {
            _camera = camera;
        }

        public void ApplyPitch(float pitch)
        {
            _camera.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
        public void ApplyFOV(float fov)
        {
            _camera.Lens.FieldOfView = fov;
        }
    }
}