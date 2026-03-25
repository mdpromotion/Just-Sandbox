public interface IGameState 
{
    bool IsDesktop { get; }
    int ToggleMenu(bool active);
}
