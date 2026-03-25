using Shared.Data;

namespace Shared.Domain
{
    public interface ITarget
    {
        Result ReceiveDamage(AttackInfo attackInfo);
    }
}