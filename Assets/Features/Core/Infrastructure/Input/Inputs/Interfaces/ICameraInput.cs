using System;
using UnityEngine;

namespace Core.PlayerInput
{
    public interface ICameraInput
    {
        event Action<Vector2> MouseMoved;
        void Tick();
    }
}