using Feature.PlayerExitMenu.Domain;
using Feature.Scene.Infrastructure;
using System.Threading.Tasks;
using UnityEngine;

namespace Feature.PlayerExitMenu.Application
{
    public class MenuUseCase : IMenuUseCase
    {
        public static readonly string LogTag = nameof(MenuUseCase);

        private readonly IMenuData _menuData;
        private readonly IToggleMenuUseCase _toggleMenu;
        private readonly ISceneManager _sceneManager;
        private readonly ILogger _logger;

        public MenuUseCase(IMenuData menuData, IToggleMenuUseCase toggleMenu, ISceneManager sceneManager, ILogger logger)
        {
            _menuData = menuData;
            _toggleMenu = toggleMenu;
            _sceneManager = sceneManager;
            _logger = logger;
        }

        public void ToggleMenu()
        {
            bool state = _menuData.ToggleMenu();

            _toggleMenu.ToggleMenu(state);
        }

        public async Task Execute(string basePath = "Assets/Scenes/Menu.unity")
        {
            var result = await _sceneManager.LoadSceneAsync(basePath);
            if (!result.IsSuccess)
            {
                _logger.LogError(LogTag, result.Error);
                return;
            }
        }
    }
}