using Shared.Data;

namespace Feature.Agent.Application
{
    /// <summary>
    /// Provides functionality for managing agent life events, including handling damage and death interactions within
    /// the game environment.
    /// </summary>
    /// <remarks>Implements the ILifeUseCase interface to process agent responses to attacks, applying
    /// knockback effects based on attack information. The knockback direction includes a fixed vertical offset to
    /// ensure consistent behavior during damage and death events. Use this class to centralize agent life event
    /// handling and maintain consistent interaction effects across agents.</remarks>
    public class LifeUseCase : ILifeUseCase
    {
        private readonly float _offsetY = 0.75f;

        public Result OnAgentDamaged(IAgentController controller, AttackInfo attackInfo)
        {
            Position3 velocity = CalculateKnockback(controller.AgentPosition, attackInfo);

            controller.Punch(velocity);
            return Result.Success();
        }
        public Result OnAgentDied(IAgentController controller, AttackInfo attackInfo)
        {
            Position3 velocity = CalculateKnockback(controller.AgentPosition, attackInfo);

            controller.Die(velocity);
            return Result.Success();
        }

        private Position3 CalculateKnockback(Position3 agentPosition, AttackInfo attackInfo)
        {
            Position3 direction = agentPosition - attackInfo.AttackerPosition;
            direction = direction.Normalize();

            direction.Y = _offsetY;

            return direction * attackInfo.Knockback;
        }
    }
}