using Feature.Agent.Infrastructure;
using Features.Agent.Application.Controllers.Interfaces;
using Features.Agent.Infrastructure.Controllers;

namespace Features.Agent.Infrastructure.Assembler.Data
{
    public readonly struct ResolverOutput
    {
        public readonly INavMeshController Controller;
        public readonly TriggerHandler TriggerHandler;

        public ResolverOutput(INavMeshController controller, TriggerHandler triggerHandler)
        {
            Controller = controller;
            TriggerHandler = triggerHandler;
        }
    }
}