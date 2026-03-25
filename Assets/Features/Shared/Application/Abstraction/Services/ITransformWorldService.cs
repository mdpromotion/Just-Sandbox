using Feature.Items.Data;

namespace Shared.Service
{
    public interface ITransformWorldService
    {
        Result<TransformProvider> GetSpawnPoint();
        Result<TransformProvider> GetDropDirection();
    }
}