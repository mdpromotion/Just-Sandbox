using Cysharp.Threading.Tasks;

namespace Feature.Toolbox.Application
{
    public interface ISpawnUseCase
    {
        UniTask TrySpawn(int id);
    }

}