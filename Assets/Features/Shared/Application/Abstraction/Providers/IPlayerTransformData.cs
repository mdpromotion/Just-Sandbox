using Shared.Data;

namespace Shared.Providers
{
    public interface IPlayerTransformData
    {
        Position3 Position { get; }
        Position3 Forward { get; }
        Position3 Right { get; }
    }
}