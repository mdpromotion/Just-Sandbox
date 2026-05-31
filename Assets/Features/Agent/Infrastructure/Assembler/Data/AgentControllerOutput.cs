using Features.Agent.Application.Controllers;

namespace Features.Agent.Infrastructure.Assembler.Data
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