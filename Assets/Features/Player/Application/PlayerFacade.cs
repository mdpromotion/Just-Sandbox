using Feature.Player.Domain;
using Shared.Data;
using Shared.Service;
using System;
using Zenject;

namespace Feature.Player.Application
{
    public class PlayerFacade : IInitializable, IDisposable
    {
        private readonly ILifeEvents _lifeEvents;
        private readonly IPhysicsController _physicsController;
        private readonly ILifeUseCase _useCase;
        private readonly IPlayerLifeController _controller;

        public PlayerFacade(
            ILifeEvents lifeEvents,
            IPhysicsController physicsController,
            ILifeUseCase useCase,
            IPlayerLifeController controller)
        {
            _lifeEvents = lifeEvents;
            _physicsController = physicsController;
            _useCase = useCase;
            _controller = controller;
        }

        public void Initialize()
        {
            _lifeEvents.ReceivedDamage += OnPlayerDamaged;
            _lifeEvents.PlayerDied += OnPlayerDied;
            _lifeEvents.PlayerRespawned += OnPlayerRespawned;
        }

        public void Dispose()
        {
            _lifeEvents.ReceivedDamage -= OnPlayerDamaged;
            _lifeEvents.PlayerDied -= OnPlayerDied;
            _lifeEvents.PlayerRespawned -= OnPlayerRespawned;
        }

        private void OnPlayerDamaged(AttackInfo info)
        {
            _useCase.OnPlayerDamaged(_physicsController, info);
        }
        private void OnPlayerDied()
        {
            _useCase.OnPlayerDied(_physicsController);
            _controller.RequestRespawn();
        }
        private void OnPlayerRespawned()
        {
            _useCase.OnPlayerRespawned(_physicsController);
        }
    }
}