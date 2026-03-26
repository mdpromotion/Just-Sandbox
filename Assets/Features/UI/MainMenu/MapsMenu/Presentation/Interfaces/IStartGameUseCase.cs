using Feature.MapsMenu.Data;
using System.Threading.Tasks;

namespace Feature.MapsMenu.Application
{
    public interface IStartGameUseCase
    {
        Task<Result> StartGame(SceneData data);
    }
}