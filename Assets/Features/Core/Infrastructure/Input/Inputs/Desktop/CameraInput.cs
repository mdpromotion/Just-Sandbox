using System;
using UnityEngine;
using Zenject;

namespace Core.PlayerInput
{
    public class DesktopCameraInput : ICameraInput
    {
        public event Action<Vector2> MouseMoved;

        public void Tick()
        {
            Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            if (delta.magnitude > 0.001f)
                MouseMoved?.Invoke(delta);
        }

    }
}