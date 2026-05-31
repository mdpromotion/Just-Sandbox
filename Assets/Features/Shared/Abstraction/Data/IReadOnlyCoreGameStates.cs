using Features.Core.Interfaces;

namespace Core.Data
{
    public interface IReadOnlyCoreGameStates
    {
        IReadOnlyGameState Game { get; }
        bool IsPlayerControllable { get; }
    }
}