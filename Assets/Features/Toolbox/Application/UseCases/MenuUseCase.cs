using Feature.Player.Domain;
using Feature.Toolbox.Domain;
using System;
using UnityEngine;

namespace Feature.Toolbox.Application
{
    /// <summary>
    /// Provides menu-related functionality for toggling the toolbox, selecting textures, and managing inventory spawn
    /// state within the game. Implements event-driven operations for menu interactions.
    /// </summary>
    /// <remarks>This class requires valid player, game, and toolbox state objects, as well as a logger, to
    /// operate correctly. The toolbox can only be toggled if the player is alive; otherwise, a warning is logged and
    /// the operation fails. MenuUseCase raises the ToolboxToggled event when the toolbox state changes, allowing
    /// subscribers to react to menu visibility updates.</remarks>
    public class MenuUseCase : IMenuUseCase, IMenuEvents
    {
        private static readonly string LogTag = nameof(MenuUseCase);

        private readonly IReadOnlyPlayer _playerState;
        private readonly IGameState _gameState;
        private readonly IToolboxState _state;
        private readonly ILogger _logger;

        public event Action<bool> ToolboxToggled;

        public MenuUseCase(
            IReadOnlyPlayer playerState,
            IGameState gameState,
            IToolboxState state,
            ILogger logger)
        {
            _playerState = playerState;
            _gameState = gameState;
            _state = state;
            _logger = logger;
        }

        public bool ToggleToolbox()
        {
            if (!_playerState.IsAlive)
            {
                _logger.LogWarning(LogTag, "Player is dead. Toolbox cannot be toggled.");
                return false;
            }

            bool active = _state.ToggleToolbox();

            _gameState.ToggleMenu(active);

            ToolboxToggled?.Invoke(active);
            return active;
        }

        public void SelectTexture(int id)
        {
            _state.SetTextureID(id);
        }

        public bool ToggleInventorySpawn()
        {
            bool enabled = !_state.SpawnToInventory;
            _state.SetSpawnToInventory(enabled);

            return enabled;
        }
    }
}