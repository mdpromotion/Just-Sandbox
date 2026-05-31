namespace Features.Combat.Presentation.Interfaces
{
    public interface IView
    {
        void SetAmmoText(string text);
        void ToggleAmmoText(bool enabled);
    }
}