using System;

namespace Feature.Agent.Application
{
    public interface IAgentLifeController
    {
        void RequestBurial(Guid entityId);
    }
}