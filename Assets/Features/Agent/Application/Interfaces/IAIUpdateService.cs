using Feature.Agent.Application;
using System;

namespace Feature.Agent.Infrastructure
{
    public interface IAIUpdateService
    {
        void RegisterAgent(Guid entityId, IAgentController controller, IDamageController damageController);
        void UnregisterAgent(Guid entityId);
    }
}