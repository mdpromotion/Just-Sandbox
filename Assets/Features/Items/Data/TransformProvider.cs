using UnityEngine;

namespace Feature.Items.Data
{
    public readonly struct TransformProvider
    {
        public readonly Vector3 Position { get; }
        public readonly Quaternion Rotation { get; }
        public TransformProvider(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}