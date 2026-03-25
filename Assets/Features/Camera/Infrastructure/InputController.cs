using Core.PlayerInput;
using Feature.PlayerCamera.Application;
using System;
using UnityEngine;
using Zenject;

namespace Feature.PlayerCamera.Infrastructure
{
    public class InputController : IInitializable, IDisposable
    {
        private readonly CameraUseCase _cameraUseCase;
        private readonly ICameraInput _cameraInput;

        public InputController(CameraUseCase cameraUseCase, ICameraInput cameraInput)
        {
            _cameraUseCase = cameraUseCase;
            _cameraInput = cameraInput;
        }

        public void Initialize()
        {
            _cameraInput.MouseMoved += OnMouseMoved;
        }

        private void OnMouseMoved(Vector2 delta)
        {
            _cameraUseCase.CameraUpdate(delta);
        }

        public void Dispose()
        {
            _cameraInput.MouseMoved -= OnMouseMoved;
        }
    }
}