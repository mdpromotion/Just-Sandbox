using Feature.Agent.Application;

namespace Feature.Agent.Infrastructure
{
    public readonly struct AgentControllerOutput
    {
        public AgentController Controller { get; }
        public DamageController DamageController { get; }

        public AgentControllerOutput(AgentController controller, DamageController damageController)
        {
            Controller = controller;
            DamageController = damageController;
        }
    }
}