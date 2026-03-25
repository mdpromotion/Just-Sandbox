using Shared.Data;

namespace Feature.Agent.Application
{
    public interface ILifeUseCase
    {
        Result OnAgentDamaged(IAgentController controller, AttackInfo attackInfo);
        Result OnAgentDied(IAgentController agentController, AttackInfo attackInfo);
    }
}