namespace Feature.ExitMenu.Domain
{
    public interface IMenuData
    {
        bool IsMenuActive { get; }
        bool ToggleMenu();
    }
}