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

        public bool TryGetPosition(Guid guid, out Position3? position)
        {
            var RawPosition = _map.FirstOrDefault(kvp => kvp.Key.Id == guid).Value?.transform.position;
            position = RawPosition.HasValue ? Mapper.ToPosition3(RawPosition.Value) : null;
            return position != null;
        }

        public List<EntityTransformData> GetEntitiesAround(Position3 position, float maxDistance)
        {
            var result = new List<EntityTransformData>(_map.Count);
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
                    result.Add(new EntityTransformData(
                        kvp.Key,
                        new Position3(pos.x, pos.y, pos.z),
                        distSq
                    ));
                }
            }

            return result;
        }

        public GameObject? GetGameObject(Guid guid)
        {
            var kvp = _map.FirstOrDefault(kvp => kvp.Key.Id == guid);
            return kvp.Value;
        }

        public void Bind(IEntity entity, GameObject go)
        {
            _map[entity] = go;
        }

        public void Unbind(Guid guid)
        {
            var entity = _map.Keys.FirstOrDefault(e => e.Id == guid);
            _map.Remove(entity);
        }
    }
}