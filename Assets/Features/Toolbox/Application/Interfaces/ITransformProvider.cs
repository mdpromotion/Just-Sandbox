using UnityEngine;

public interface ITransformProvider
{
    Vector3 Position { get; }
    Vector3 Forward { get; }
}
