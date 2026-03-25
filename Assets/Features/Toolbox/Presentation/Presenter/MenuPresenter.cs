using Feature.Toolbox.Domain;
using System;
using Zenject;

namespace Feature.Toolbox.Presentation
{
    public class MenuPresenter : IInitializable, IDisposable
    {
        private readonly IToggleMenuUseCase _toolboxMenu;
        private readonly IMenuEvents _menuEvents;

        private IView _view;

        private int _currentTabIndex = -1;

        public MenuPresenter(IView view, IToggleMenuUseCase toolboxMenu, IMenuEvents menuEvents)
        {
            _view = view;
            _toolboxMenu = toolboxMenu;
            _menuEvents = menuEvents;
        }

        public void Initialize()
        {
            _view.TabButtonClicked += OnTabButtonClicked;
            _menuEvents.ToolboxToggled += OnToolboxToggled;

            OnTabButtonClicked(0);
        }

        private void OnToolboxToggled(bool enabled)
        {
            _toolboxMenu.ToggleMenu(enabled);
            _view.ToggleToolbox(enabled);
        }

        private void OnTabButtonClicked(int id)
        {
            int previousIndex = _currentTabIndex;

            if (previousIndex != -1)
                ToggleTab(previousIndex, false);

            _currentTabIndex = id;

            ToggleTab(_currentTabIndex, true);
        }

        private void ToggleTab(int index, bool active)
        {
            _view.ToggleTab(index, active);
            _view.ToggleButton(index, active);
        }

        public void Dispose()
        {
            _view.TabButtonClicked -= OnTabButtonClicked;
            _menuEvents.ToolboxToggled -= OnToolboxToggled;
        }
    }
}