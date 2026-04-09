using Cysharp.Threading.Tasks;

namespace Feature.Scene.Infrastructure
{
    public interface ISceneManager
    {
        UniTask<Result> LoadSceneAsync(string path);
    }
}