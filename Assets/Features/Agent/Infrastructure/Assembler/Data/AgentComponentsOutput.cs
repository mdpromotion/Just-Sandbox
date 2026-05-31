using Feature.Agent.Infrastructure;
using Features.Agent.Infrastructure.Controllers;

namespace Features.Agent.Infrastructure.Assembler.Data
{
    public readonly struct AgentComponentsOutput
    {
        public NavMeshController NavMesh { get; }
        public TriggerHandler TriggerHandler { get; }

        public AgentComponentsOutput(NavMeshController navMesh, TriggerHandler triggerHandler)
        {
            NavMesh = navMesh;
            TriggerHandler = triggerHandler;
        }
    }
}
