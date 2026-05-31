using Features.Combat.Presentation.Interfaces;

namespace Features.Combat.Presentation.View
{
    public class View : IView
    {
        private readonly AmmoView _ammoView;

        public View(AmmoView ammoView)
        {
            _ammoView = ammoView;
        }

        public void SetAmmoText(string text)
        {
            _ammoView.SetAmmoText(text);
        }

        public void ToggleAmmoText(bool enabled)
        {
            _ammoView.SetActive(enabled);
        }
    }
}