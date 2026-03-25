using Shared.Domain;
using Shared.Service;

namespace Feature.Player.Application
{
    public class PlayerLifeController : IPlayerLifeController
    {
        private readonly IRespawnable _entity;
        private readonly IDelay _delayService;

        private readonly float _respawnAfterSeconds = 5f;

        public PlayerLifeController(Domain.Player player, IDelay delayService, float respawnAfterSeconds = 5f)
        {
            _entity = player;
            _delayService = delayService;
            _respawnAfterSeconds = respawnAfterSeconds;
        }

        public void RequestRespawn()
        {
            _delayService.ExecuteAfterDelay(_respawnAfterSeconds, () => _entity.Respawn());
        }
    }
}