using UnityEngine;
using UnityEngine.UI;

namespace Feature.Player.Presentation
{
    /// <summary>
    /// Represents the visual component responsible for displaying a character's health and death state in the user
    /// interface.
    /// </summary>
    /// <remarks>The LifeView class manages the health bar and death screen overlays for a character or
    /// entity. It provides methods to update the health bar's fill amount and to adjust the transparency of the death
    /// screen based on the current health state. This class is typically used in conjunction with gameplay logic to
    /// reflect changes in health visually to the player.</remarks>
    public class LifeView : MonoBehaviour, ILifeView
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _deathScreen;

        public void SetHealth(float percent)
        {
            if (_healthBar != null)
                _healthBar.fillAmount = percent;
        }

        public void SetDeathScreenTransparency(float percent)
        {
            if (_healthBar != null)
                _deathScreen.color = new Color(_deathScreen.color.r, _deathScreen.color.g, _deathScreen.color.b, percent);
        }

        public float CurrentFillAmount()
        {
            if (_healthBar != null)
                return _healthBar.fillAmount;
            else return 0f;
        }
    }
}
