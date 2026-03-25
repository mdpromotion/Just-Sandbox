using Feature.Agent.Infrastructure;
using Shared.Data;
using System;
using UnityEngine;

namespace Feature.Agent.Application
{
    public interface IWorldAgentUseCase
    {
        Result<AgentContext> SpawnNPC(IAgentProvider agentProvider, GameObject obj);
        Result DespawnNPC(Guid agentId);
    }
}