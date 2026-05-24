using Core.Service;
using Core.Service.Data;
using Shared.Data;
using Shared.Domain;
using System.Collections.Generic;

namespace Feature.Agent.Application 
{
    /// <summary>
    /// Provides navigation functionality for an entity, including the ability to locate the nearest entity within a
    /// specified vision range that is not on the same team as the controlled entity.
    /// </summary>
    /// <remarks>Use this controller to assist AI agents or game logic in detecting and interacting with
    /// nearby entities in the game world. The vision range should be set according to the desired detection radius for
    /// the entity. This class relies on an external entity service to retrieve entities and does not manage entity
    /// lifecycles itself.</remarks>
    public class NavigationController
    {
        private readonly IWorldEntityService _entityService;
        private readonly IEntity _entity;
        private readonly float _visionRange;

        private readonly List<EntityTransformData> _entitiesBuffer = new(32);

        public NavigationController(IWorldEntityService entityService, IEntity entity, float visionRange)
        {
            _entityService = entityService;
            _entity = entity;
            _visionRange = visionRange;
        }

        public bool FindNearestEntity(Position3 agentPosition, out EntityTransformData nearestEntity)
        {
            nearestEntity = default;

            _entityService.GetEntitiesAround(agentPosition, _visionRange, _entitiesBuffer);

            if (_entitiesBuffer.Count == 0)
                return false;

            bool found = false;
            float minDistance = float.MaxValue;

            for (int i = 0; i < _entitiesBuffer.Count; i++)
            {
                var entityData = _entitiesBuffer[i];

                if (entityData.Entity.Team == _entity.Team)
                    continue;

                var distance = Position3.Distance(agentPosition, entityData.Position);
                if (!found || distance < minDistance)
                {
                    minDistance = distance;
                    nearestEntity = new EntityTransformData(entityData.Entity, entityData.Position, distance);
                    found = true;
                }
            }

            return found;
        }
    }
}
