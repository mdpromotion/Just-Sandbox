using Feature.UI.Maps.Data;
using System.Threading.Tasks;

namespace Feature.UI.Maps.Presentation
{
    public interface IStartGameUseCase
    {
        Task<Result> StartGame(SceneData data);
    }
}