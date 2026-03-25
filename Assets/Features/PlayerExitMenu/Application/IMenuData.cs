namespace Feature.PlayerExitMenu.Domain
{
    public interface IMenuData
    {
        bool IsMenuActive { get; }
        bool ToggleMenu();
    }
}