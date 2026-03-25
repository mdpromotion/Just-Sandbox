using Feature.Toolbox.Data;
using System.Threading.Tasks;

namespace Feature.Toolbox.Application
{
    public interface INPCSpawnUseCase
    {
        Task<Result<AgentSpawnContext>> TrySpawnNPC(int id);
    }
}