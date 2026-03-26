using Cysharp.Threading.Tasks;
using Feature.Toolbox.Data;

namespace Feature.Toolbox.Application
{
    public interface IItemSpawnUseCase
    {
        UniTask<Result<ItemSpawnContext>> TrySpawnObject(int objectId, int textureId);
    }
}