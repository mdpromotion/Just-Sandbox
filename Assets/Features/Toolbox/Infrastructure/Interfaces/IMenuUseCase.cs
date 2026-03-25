namespace Feature.Toolbox.Application
{
    public interface IMenuUseCase
    {
        bool ToggleToolbox();
        void SelectTexture(int id);
        bool ToggleInventorySpawn();
    }
}