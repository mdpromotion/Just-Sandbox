using System.Threading.Tasks;

namespace Feature.Scene.Infrastructure
{
    public interface ISceneManager
    {
        Task<Result> LoadSceneAsync(string path);
    }
}