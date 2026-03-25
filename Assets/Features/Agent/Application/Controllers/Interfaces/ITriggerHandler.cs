using Shared.Domain;
using System;

namespace Feature.Agent.Infrastructure
{
    public interface ITriggerHandler
    {
        event Action<ITarget> TargetEntered;
        event Action<ITarget> TargetExited;
    }
}