using Core.Service;
using Feature.Items.Data;
using Shared.Providers;
using Shared.Service;
using UnityEngine;

namespace Feature.Toolbox.Infrastructure
{
    public class TransformWorldService : ITransformWorldService
    {
        private readonly ICameraTransformData _transform;
        private readonly IRaycastService _raycastService;

        private readonly float _spawnDistance;
        private readonly float _dropDistance;

        public TransformWorldService(
            ICameraTransformData transform,
            IRaycastService raycastService,
            float spawnDistance = 5f,
            float dropDistance = 3f)
        {
            _transform = transform;
            _raycastService = raycastService;
            _spawnDistance = spawnDistance;
            _dropDistance = dropDistance;
        }

        public Result<TransformProvider> GetSpawnPoint()
        {
            Vector3 position = CalculatePosition(_spawnDistance);
            Quaternion rotation = GetSpawnRotation(position);

            return Result<TransformProvider>.Success(new TransformProvider(position, rotation));
        }

        public Result<TransformProvider> GetDropDirection()
        {
            Vector3 position = CalculatePosition(_dropDistance);
            Quaternion rotation = GetSpawnRotation(position);

            return Result<TransformProvider>.Success(new TransformProvider(position, rotation));
        }

        private Vector3 CalculatePosition(float distance)
        {
            Vector3 origin = _transform.Position;
            Vector3 direction = _transform.Forward;

            Vector3 spawnPosition = _raycastService.GetRaycastPosition(origin, direction, distance);

            return spawnPosition;
        }

        private Quaternion GetSpawnRotation(Vector3 spawnPosition)
        {
            Vector3 lookDirection = _transform.Position - spawnPosition;
            lookDirection.y = 0;

            if (lookDirection.sqrMagnitude < 0.001f)
                return Quaternion.identity;

            Quaternion lookRotation = Quaternion.LookRotation(lookDirection.normalized);
            return lookRotation;

        }
    }
}