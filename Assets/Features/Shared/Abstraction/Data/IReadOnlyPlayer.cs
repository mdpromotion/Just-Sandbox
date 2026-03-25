using UnityEngine;

namespace Feature.Player.Domain
{
    public interface IReadOnlyPlayer
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }
        bool IsAlive { get; }
    }
}