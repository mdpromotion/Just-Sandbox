using Cysharp.Threading.Tasks;

namespace Feature.Scene.Application
{
    public interface ILoadSceneUseCase
    {
        UniTask LoadScene(string pathData);
    }
}