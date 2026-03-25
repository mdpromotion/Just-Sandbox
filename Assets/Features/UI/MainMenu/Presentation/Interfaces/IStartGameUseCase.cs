using Feature.MainMenu.Data;
using System.Threading.Tasks;

namespace Feature.MainMenu.Application
{
    public interface IStartGameUseCase
    {
        Task<Result> StartGame(SceneData data);
    }
}