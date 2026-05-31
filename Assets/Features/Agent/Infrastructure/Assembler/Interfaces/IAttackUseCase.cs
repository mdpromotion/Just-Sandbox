using Feature.Player.Data;

namespace Features.Agent.Infrastructure.Assembler.Interfaces
{
    public interface IAttackUseCase
    {
        void Attack(AttackData data);
    }
}