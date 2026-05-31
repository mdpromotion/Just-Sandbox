using Feature.Agent.Application;
using Features.Agent.Domain.Interfaces;
using Shared.Data;

namespace Features.Agent.Infrastructure.Assembler.Interfaces
{
    public interface ILifeUseCase
    {
        Result OnAgentDamaged(IAgentController controller, AttackInfo attackInfo);
        Result OnAgentDied(IAgentController agentController, AttackInfo attackInfo);
    }
}