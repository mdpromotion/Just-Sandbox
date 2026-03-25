using Shared.Data;
using Shared.Domain;
using System;

namespace Core.Service.Data
{
    public readonly struct EntityTransformData
    {
        public IEntity Entity { get; }
        public Position3 Position { get; }
        public float DistanceToTarget { get; }

        public EntityTransformData(IEntity entity, Position3 position, float distanceToTarget)
        {
            Entity = entity;
            Position = position;
            DistanceToTarget = distanceToTarget;
        }
    }
}