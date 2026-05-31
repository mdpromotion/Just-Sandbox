#nullable enable
using System;
using System.Collections.Generic;
using Core.Service;
using Features.Core.Infrastructure.Services.Data;
using Features.Core.Interfaces;
using Shared.Data;
using Shared.Domain;
using UnityEngine;

namespace Features.Core.Infrastructure.Services
{
    public class WorldEntityService : IWorldEntityService
    {
        private readonly Dictionary<IEntity, GameObject> _map = new();
        private readonly Dictionary<GameObject, IEntity> _gameObjectToEntity = new();
        private readonly Dictionary<Guid, IEntity> _guidToEntity = new();
        private readonly Collider[] _physicsBuffer = new Collider[512];
        private readonly int _entityLayerMask = LayerMask.GetMask("NPC", "Player");

        public bool TryGetPosition(Guid guid, out Position3? position)
        {
            position = null;

            if (!_guidToEntity.TryGetValue(guid, out var entity)) return false;
            if (!_map.TryGetValue(entity, out var go) || go == null) return false;
            position = go.transform.position.ToPosition3();
            return true;
        }
        public void GetEntitiesAround(Position3 position, float maxDistance, List<EntityTransformData> resultsBuffer)
        {
            resultsBuffer.Clear();
            var center = position.ToVector3();

            var hitCount = Physics.OverlapSphereNonAlloc(
                center,
                maxDistance,
                _physicsBuffer,
                _entityLayerMask,
                QueryTriggerInteraction.Ignore
            );

            for (var i = 0; i < hitCount; i++)
            {
                var hitCollider = _physicsBuffer[i];
                if (hitCollider == null) continue;

                if (_gameObjectToEntity.TryGetValue(hitCollider.gameObject, out var foundEntity))
                {
                    var entityPos = hitCollider.transform.position;
                    var distSq = Vector3.SqrMagnitude(center - entityPos);

                    resultsBuffer.Add(new EntityTransformData(
                        foundEntity,
                        Mapper.ToPosition3(entityPos),
                        distSq
                    ));
                }
            }

            Array.Clear(_physicsBuffer, 0, hitCount);
        }

        public GameObject? GetGameObject(Guid guid)
        {
            return _guidToEntity.TryGetValue(guid, out var entity) ? null : _map.GetValueOrDefault(entity);
        }

        public void Bind(IEntity entity, GameObject go)
        {
            _map[entity] = go;
            _guidToEntity[entity.Id] = entity;
            _gameObjectToEntity[go] = entity;
        }

        public void Unbind(Guid guid)
        {
            if (_guidToEntity.TryGetValue(guid, out var entity))
            {
                if (_map.TryGetValue(entity, out var go) && go != null)
                {
                    _gameObjectToEntity.Remove(go);
                }
                _map.Remove(entity);
                _guidToEntity.Remove(guid);
            }
        }
    }
}