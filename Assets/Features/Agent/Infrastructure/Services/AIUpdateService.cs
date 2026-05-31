using System;
using System.Collections.Generic;
using Features.Agent.Application.Interfaces;
using Features.Agent.Domain.Interfaces;
using Features.Agent.Infrastructure.Services.Interfaces;
using Zenject;

namespace Features.Agent.Infrastructure.Services
{
    public class AIUpdateService : ITickable, IAIUpdateService
    {
        private readonly Dictionary<Guid, IAgentController> _controllers = new();
        private readonly Dictionary<Guid, IDamageController> _damageControllers = new();

        public void RegisterAgent(Guid entityId, IAgentController controller, IDamageController damageController)
        {
            _controllers.TryAdd(entityId, controller);

            _damageControllers.TryAdd(entityId, damageController);
        }

        public void UnregisterAgent(Guid entityId) 
        {
            _controllers.Remove(entityId);
            _damageControllers.Remove(entityId);
        }
        
        public void Tick()
        {
            foreach (var agent in _controllers)
            {
                agent.Value.Tick();
            }
            foreach (var damageController in _damageControllers)
            {
                damageController.Value.Tick();
            }
        }
    }
}