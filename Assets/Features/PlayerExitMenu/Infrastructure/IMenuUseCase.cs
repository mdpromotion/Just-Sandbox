using System.Threading.Tasks;

namespace Feature.PlayerExitMenu.Application
{
    public interface IMenuUseCase
    {
        void ToggleMenu();
        Task LoadScene(string path);
    }
}