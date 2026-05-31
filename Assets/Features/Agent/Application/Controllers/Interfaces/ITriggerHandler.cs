using System;
using Shared.Domain;

namespace Features.Agent.Application.Controllers.Interfaces
{
    public interface ITriggerHandler
    {
        event Action<ITarget> TargetEntered;
        event Action<ITarget> TargetExited;
    }
}