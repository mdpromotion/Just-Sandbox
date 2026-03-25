using Shared.Providers;
using Unity.Cinemachine;
using UnityEngine;

namespace Feature.PlayerCamera.Infrastructure
{
    public class TransformProvider : ICameraTransformData
    {
        private CinemachineCamera _camera;

        public Vector3 Position => _camera.transform.position;
        public Vector3 Forward => _camera.transform.forward;
        public Vector3 Right => _camera.transform.right;
        public Vector3 Up => _camera.transform.up;

        public TransformProvider(CinemachineCamera camera)
        {
            _camera = camera;
        }
    }
}