public interface IReadOnlyGameState
{
    int OpenedMenus { get; }
    bool IsDesktop { get; }
    Language CurrentLanguage { get; }
    bool IsPaused { get; }
}
