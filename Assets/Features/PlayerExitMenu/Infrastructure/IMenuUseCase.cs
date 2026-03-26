using Cysharp.Threading.Tasks;

namespace Feature.PlayerExitMenu.Application
{
    public interface IMenuUseCase
    {
        void ToggleMenu();
        UniTask LoadScene(string path);
    }
}