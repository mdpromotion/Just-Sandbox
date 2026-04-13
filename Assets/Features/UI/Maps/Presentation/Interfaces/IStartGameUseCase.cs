using Cysharp.Threading.Tasks;
using Feature.UI.Maps.Data;

namespace Feature.UI.Maps.Presentation
{
    public interface IStartGameUseCase
    {
        UniTask<Result> StartGame(SceneData data);
    }
}