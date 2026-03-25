
public enum Language
{
    English,
    Russian,
    Spanish
}

public class GameState : IGameState, IReadOnlyGameState
{
    public int OpenedMenus { get; private set; }
    public bool IsDesktop { get; private set; }
    public Language CurrentLanguage { get; private set; }
    public bool IsPaused => OpenedMenus > 0;

    public GameState(bool isDesktop = true, Language language = Language.English)
    {
        IsDesktop = isDesktop;
        CurrentLanguage = language;
    }

    public int ToggleMenu(bool active)
    {
        if (active)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
        return OpenedMenus;
    }
    private void OpenMenu() => OpenedMenus++;
    private void CloseMenu()
    {
        if (OpenedMenus > 0)
        {
            OpenedMenus--;
        }
    }
}
