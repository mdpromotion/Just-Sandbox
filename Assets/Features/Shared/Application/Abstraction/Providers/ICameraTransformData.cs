using UnityEngine;

namespace Shared.Providers
{
    public interface ICameraTransformData
    {
        Vector3 Position { get; }
        Vector3 Forward { get; }
        Vector3 Right { get; }
        Vector3 Up { get; }
    }
}