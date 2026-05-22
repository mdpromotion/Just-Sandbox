#nullable enable
using Core.Service.Data;
using Shared.Data;
using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Service
{
    public class WorldEntityService : IWorldEntityService
    {
        private readonly Dictionary<IEntity, GameObject> _map = new();
        private readonly Dictionary<Guid, IEntity> _guidToEntity = new();
        private readonly Collider[] _physicsBuffer = new Collider[512];

        public bool TryGetPosition(Guid guid, out Position3? position)
        {
            position = null;

            if (_guidToEntity.TryGetValue(guid, out var entity))
            {
                if (_map.TryGetValue(entity, out var go) && go != null)
                {
                    position = Mapper.ToPosition3(go.transform.position);
                    return true;
                }
            }
            return false;
        }
        public void GetEntitiesAround(Position3 position, float maxDistance, List<EntityTransformData> resultsBuffer)
        {
            resultsBuffer.Clear();
            float maxDistSq = maxDistance * maxDistance;

            foreach (var kvp in _map)
            {
                var transform = kvp.Value.transform;
                var pos = transform.position;

                float dx = pos.x - position.X;
                float dy = pos.y - position.Y;
                float dz = pos.z - position.Z;

                float distSq = dx * dx + dy * dy + dz * dz;

                if (distSq <= maxDistSq)
                {
                    resultsBuffer.Add(new EntityTransformData(
                        kvp.Key,
                        new Position3(pos.x, pos.y, pos.z),
                        distSq
                    ));
                }
            }
        }

        public GameObject? GetGameObject(Guid guid)
        {
            if (!_guidToEntity.TryGetValue(guid, out var entity))
            {
                if (_map.TryGetValue(entity, out var go))
                {
                    return go;
                }
            }
            return null;
        }

        public void Bind(IEntity entity, GameObject go)
        {
            _map[entity] = go;
            _guidToEntity[entity.Id] = entity;
        }

        public void Unbind(Guid guid)
        {
            if (_guidToEntity.TryGetValue(guid, out var entity))
            {
                _map.Remove(entity);
                _guidToEntity.Remove(guid);
            }
        }
    }
}