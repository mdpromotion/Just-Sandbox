using Feature.Toolbox.Data;
using System.Threading.Tasks;
namespace Feature.Toolbox.Application
{
    public interface IItemSpawnUseCase
    {
        Task<Result<ItemSpawnContext>> TrySpawnObject(int objectId, int textureId);
    }
}