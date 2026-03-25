using Feature.Player.Data;

namespace Feature.Agent.Application
{
    public interface IAttackUseCase
    {
        void Attack(AttackData data);
    }
}