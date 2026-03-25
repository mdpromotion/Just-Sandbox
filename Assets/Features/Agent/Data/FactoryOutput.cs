namespace Feature.Agent.Data
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
        public Domain.Agent Agent { get; }
        public Domain.AgentFSM FSM { get; }
        public Application.AgentFacade Facade { get; }
        public Application.AgentController Controller { get; }
        public Application.DamageController DamageController { get; }

        public FactoryOutput(
            Domain.Agent agent, 
            Domain.AgentFSM fsm, 
            Application.AgentFacade facade,
            Application.AgentController controller,
            Application.DamageController damageController)
        {
            Agent = agent;
            FSM = fsm;
            Facade = facade;
            Controller = controller;
            DamageController = damageController;
        }
    }
}