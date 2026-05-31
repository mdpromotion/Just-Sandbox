#nullable enable
using System;
using System.Collections.Generic;
using Features.Core.Infrastructure.Services.Data;
using Shared.Data;
using Shared.Domain;
using UnityEngine;

namespace Features.Core.Interfaces
{
    public interface IWorldEntityService
    {
        bool TryGetPosition(Guid guid, out Position3? position);
        void GetEntitiesAround(Position3 position, float maxDistance, List<EntityTransformData> resultsBuffer);
        GameObject? GetGameObject(Guid guid);
        void Bind(IEntity entity, GameObject go);
        void Unbind(Guid guid);
    }
}