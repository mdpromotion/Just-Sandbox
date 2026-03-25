using System;

namespace Shared.Domain
{
    public enum Team { Enemy, Neutral, Friendly }

    public interface IEntity
    {
        Guid Id { get; }
        bool IsAlive { get; }
        Team Team { get; }
    }
}