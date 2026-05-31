using Features.Agent.Application;
using Features.Agent.Application.Controllers;

namespace Features.Agent.Data
{
    /// <summary>
    /// Encapsulates the set of components produced by an agent factory, including the agent instance, its finite state
    /// machine, and related controllers and facades.
    /// </summary>
    /// <remarks>Use this struct to access all primary objects required to interact with and manage an agent's
    /// behavior and state. The properties provide references to the agent, its state machine, and associated
    /// controllers, enabling coordinated operations and lifecycle management.</remarks>
    public readonly struct FactoryOutput
    {
        public Feature.Agent.Domain.Agent Agent { get; }
        public Feature.Agent.Domain.AgentFSM Fsm { get; }
        public AgentFacade Facade { get; }
        public AgentController Controller { get; }
        public DamageController DamageController { get; }

        public FactoryOutput(
            Feature.Agent.Domain.Agent agent, 
            Feature.Agent.Domain.AgentFSM fsm, 
            AgentFacade facade,
            AgentController controller,
            DamageController damageController)
        {
            Agent = agent;
            Fsm = fsm;
            Facade = facade;
            Controller = controller;
            DamageController = damageController;
        }
    }
}