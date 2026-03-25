
using Shared.Data;

namespace Feature.Player.Application
{
    public interface ILifeUseCase
    {
        void OnPlayerDamaged(IPhysicsController controller, AttackInfo attackInfo);
        void OnPlayerDied(IPhysicsController controller);
        void OnPlayerRespawned(IPhysicsController controller);
    }
}