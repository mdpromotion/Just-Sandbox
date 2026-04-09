using Shared.Providers;
using Zenject;

public class ToggleMenuUseCase : IToggleMenuUseCase, IInitializable
{
    private readonly IGameState _gameState;
    private readonly ICursorProvider _cursor;

    public ToggleMenuUseCase(IGameState gameState, ICursorProvider cursor)
    {
        _gameState = gameState;
        _cursor = cursor;
    }

    public void Initialize()
    {
        _cursor.ToggleCursor(false);
    }

    public void ToggleMenu(bool active)
    {
        int openedMenus = _gameState.ToggleMenu(active);
        if (_gameState.IsDesktop)
        {
            if (openedMenus == 0)
                _cursor.ToggleCursor(false);
            else
                _cursor.ToggleCursor(true);
        }
    }
}
