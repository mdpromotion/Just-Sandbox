#nullable enable
using Core.Service.Data;
using Shared.Data;
using Shared.Domain;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Service
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