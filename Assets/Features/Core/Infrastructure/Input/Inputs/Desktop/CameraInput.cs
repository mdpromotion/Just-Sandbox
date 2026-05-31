using System;
using Core.PlayerInput;
using Features.Core.Infrastructure.Input.Inputs.Interfaces;
using UnityEngine;

namespace Features.Core.Infrastructure.Input.Inputs.Desktop
{
    public class DesktopCameraInput : ICameraInput
    {
        public event Action<Vector2> MouseMoved;

        public void Tick()
        {
            Vector2 delta = new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));
            if (delta.magnitude > 0.001f)
                MouseMoved?.Invoke(delta);
        }

    }
}