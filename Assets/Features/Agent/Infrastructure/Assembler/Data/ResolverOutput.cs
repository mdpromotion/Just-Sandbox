namespace Feature.Agent.Infrastructure
{
    public readonly struct ResolverOutput
    {
        public readonly NavMeshController Controller;
        public readonly TriggerHandler TriggerHandler;

        public ResolverOutput(NavMeshController controller, TriggerHandler triggerHandler)
        {
            Controller = controller;
            TriggerHandler = triggerHandler;
        }
    }
}