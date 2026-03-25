using System;
using UnityEngine;

namespace Shared.Data
{
    public readonly struct AgentContext
    {
        public GameObject Object { get; }
        public int ConfigId { get; }
        public Guid AgentId { get; }
        public AgentContext(GameObject obj, int configId, Guid agentId)
        {
            Object = obj;
            ConfigId = configId;
            AgentId = agentId;
        }
    }
}