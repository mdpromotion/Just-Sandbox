using Shared.Data;

namespace Feature.Player.Application
{
    public interface IPunchCalculator
    {
        Position3 CalculatePunchVelocity(Position3 attackerPosition, Position3 playerPosition, float force);
    }
}