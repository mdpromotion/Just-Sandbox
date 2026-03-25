using Shared.Data;
using Shared.Providers;
using UnityEngine;

namespace Feature.Player.Application
{
    public sealed class MovementCalculator : IMovementCalculator
    {
        private readonly float _sprintMultiplier;
        private readonly float _flightMultiplier;

        public MovementCalculator(
            float sprintMultiplier = 2.5f,
            float flightMultiplier = 5f)
        {
            _sprintMultiplier = sprintMultiplier;
            _flightMultiplier = flightMultiplier;
        }

        public Position3 CalculateGroundVelocity(
            Position2 moveInput,
            IPlayerTransformData transform,
            float speed,
            float jumpPower,
            float velocityY,
            bool sprint,
            bool jump
            )
        {
            Position3 direction =
                transform.Right * moveInput.X +
                transform.Forward * moveInput.Y;

            direction.Y = 0f;

            float horizontalLength = direction.Length();
            if (horizontalLength > 1f)
                direction = direction / horizontalLength;

            float multiplier = sprint ? _sprintMultiplier : 1f;

            Position3 velocity = direction * speed * multiplier;

            velocity.Y = jump ? Mathf.Sqrt(2f * jumpPower * -Physics.gravity.y) : velocityY;

            return velocity;
        }

        public Position3 CalculateFlightVelocity(
            Position2 moveInput,
            IPlayerTransformData transform,
            float verticalInput,
            float speed)
        {
            Position3 direction =
                transform.Right * moveInput.X +
                transform.Forward * moveInput.Y;

            if (direction.SqrMagnitude > 1f)
                direction.Normalize();

            Position3 velocity = direction + Position3.UnitY * verticalInput;

            return velocity * speed * _flightMultiplier;
        }
    }
}