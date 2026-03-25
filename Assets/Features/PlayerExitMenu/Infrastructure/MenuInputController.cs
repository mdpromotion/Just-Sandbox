using Core.PlayerInput;
using Feature.PlayerExitMenu.Application;
using System;
using Zenject;

namespace Feature.PlayerExitMenu.Infrastructure
{
    public class MenuInputController : IInitializable, IDisposable
    {
        private readonly IMenuUseCase _menuUseCase;
        private readonly ISpecialButtonInput _specialButtonInput;

        public MenuInputController(
            IMenuUseCase menuUseCase,
            ISpecialButtonInput specialButtonInput)
        {
            _menuUseCase = menuUseCase;
            _specialButtonInput = specialButtonInput;
        }

        public void Initialize()
        {
            _specialButtonInput.ExitMenuPressed += OnExitMenuPressed;
        }

        private void OnExitMenuPressed()
        {
            _menuUseCase.ToggleMenu();
        }

        public void Dispose() 
        {
            _specialButtonInput.ExitMenuPressed -= OnExitMenuPressed;
        }

    }
}