using Core.Service;
using Core.Service.Data;
using Shared.Data;
using Shared.Domain;

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

        public NavigationController(IWorldEntityService entityService, IEntity entity, float visionRange)
        {
            _entityService = entityService;
            _entity = entity;
            _visionRange = visionRange;
        }

        public EntityTransformData? FindNearestEntity(Position3 agentPosition)
        {
            var entites = _entityService.GetEntitiesAround(agentPosition, _visionRange);
            if (entites == null)
                return null;

            EntityTransformData? nearestEntity = null;
            foreach (var entity in entites)
            {
                if (entity.Entity.Team == _entity.Team)
                    continue;

                var distance = Position3.Distance(agentPosition, entity.Position);
                if (nearestEntity == null || distance < nearestEntity.Value.DistanceToTarget)
                {
                    nearestEntity = new EntityTransformData(entity.Entity, entity.Position, distance);
                }
            }

            return nearestEntity;
        }
    }
}
