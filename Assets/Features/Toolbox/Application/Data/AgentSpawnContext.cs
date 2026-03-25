using Feature.Agent.Infrastructure;
using Feature.Items.Infrastructure;
using UnityEngine;

namespace Feature.Toolbox.Data
{
    public readonly struct AgentSpawnContext
    {
        public GameObject Object { get; }
        public IAgentProvider Config { get; }
        public AgentSpawnContext(GameObject obj, IAgentProvider config)
        {
            Object = obj;
            Config = config;
        }
    }
}