namespace Feature.Agent.Infrastructure
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
