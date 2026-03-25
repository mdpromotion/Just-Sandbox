using Shared.Data;

namespace Feature.Player.Application
{
    public class PunchCalculator : IPunchCalculator
    {
        private readonly float _punchUpBias;
        public PunchCalculator(float punchUpBias = 0.5f)
        {
            _punchUpBias = punchUpBias;
        }

        public Position3 CalculatePunchVelocity(
            Position3 attackerPosition,
            Position3 playerPosition,
            float force)
        {
            Position3 direction = playerPosition - attackerPosition;
            direction.Y = _punchUpBias;

            return direction.Normalize() * force;
        }
    }
}