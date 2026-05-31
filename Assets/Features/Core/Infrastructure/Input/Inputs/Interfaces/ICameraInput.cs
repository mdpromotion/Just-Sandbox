using System;
using UnityEngine;

namespace Features.Core.Infrastructure.Input.Inputs.Interfaces
{
    public interface ICameraInput
    {
        event Action<Vector2> MouseMoved;
        void Tick();
    }
}