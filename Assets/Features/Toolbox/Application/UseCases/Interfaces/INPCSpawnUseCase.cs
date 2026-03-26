using Cysharp.Threading.Tasks;
using Feature.Toolbox.Data;

namespace Feature.Toolbox.Application
{
    public interface INPCSpawnUseCase
    {
        UniTask<Result<AgentSpawnContext>> TrySpawnNPC(int id);
    }
}