using UnityEngine;

namespace Feature.Player.Application
{
    public class PlayerWorldState : IReadOnlyPlayerWorldState
    {
        public bool IsGrounded { get; set; }
    }
}