using System.Threading.Tasks;

namespace Feature.PlayerExitMenu.Application
{
    public interface IMenuUseCase
    {
        void ToggleMenu();
        Task Execute(string basePath = "Assets/Scenes/Menu.unity");
    }
}