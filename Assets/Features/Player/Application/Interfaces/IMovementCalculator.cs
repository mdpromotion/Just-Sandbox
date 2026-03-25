using Shared.Data;
using Shared.Providers;
using UnityEngine;

namespace Feature.Player.Application
{
    public interface IMovementCalculator
    {
        Position3 CalculateGroundVelocity(Position2 moveInput, IPlayerTransformData transform, float speed, float jumpPower, float velocityY, bool sprint, bool jump);
        public Position3 CalculateFlightVelocity(Position2 moveInput, IPlayerTransformData transform, float verticalInput, float speed);
    }
}
