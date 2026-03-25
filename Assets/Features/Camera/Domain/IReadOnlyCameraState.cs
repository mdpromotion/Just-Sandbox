using UnityEngine;

namespace Feature.PlayerCamera.Domain
{
    public interface IReadOnlyCameraState
    {
        float Yaw { get; }
        float Pitch { get; }
    }
}