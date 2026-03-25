using Feature.Agent.Application;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Agent.Infrastructure
{
    public class AIUpdateService : ITickable, IAIUpdateService
    {
        private readonly Dictionary<Guid, IAgentController> _controllers = new();
        private readonly Dictionary<Guid, IDamageController> _damageControllers = new();

        public void RegisterAgent(Guid entityId, IAgentController controller, IDamageController damageController)
        {
            if (!_controllers.ContainsKey(entityId))
                _controllers.Add(entityId, controller);

            if (!_damageControllers.ContainsKey(entityId))
                _damageControllers.Add(entityId, damageController);
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