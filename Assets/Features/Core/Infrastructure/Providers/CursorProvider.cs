using Shared.Providers;
using UnityEngine;

namespace Core.Providers
{
    public class CursorProvider : ICursorProvider
    {
        private readonly bool _isDesktop;
        public CursorProvider(IReadOnlyGameState gameState)
        {
            _isDesktop = gameState.IsDesktop;
        }

        public void ToggleCursor(bool isEnabled)
        {
            if (isEnabled)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (_isDesktop)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}