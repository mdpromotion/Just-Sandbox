using UnityEngine;

public interface IGameController
{
    IReadOnlyGameState GameState { get; }
    void SetPaused(bool isPaused);
    void SetOpenedMenus(int openedMenus);
    void SetIsDesktop(bool isDesktop);
    void SetCurrentLanguage(Language language);
}
