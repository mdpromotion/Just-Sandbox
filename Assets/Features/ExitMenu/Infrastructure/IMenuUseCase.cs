using Cysharp.Threading.Tasks;

namespace Feature.ExitMenu.Application
{
    public interface IMenuUseCase
    {
        void ToggleMenu();
        UniTask LoadScene(string path);
    }
}