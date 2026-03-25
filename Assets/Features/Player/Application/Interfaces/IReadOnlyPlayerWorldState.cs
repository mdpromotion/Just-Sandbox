using UnityEngine;

namespace Feature.Player.Application
{
    public interface IReadOnlyPlayerWorldState
    {
        bool IsGrounded { get; }
    }
}