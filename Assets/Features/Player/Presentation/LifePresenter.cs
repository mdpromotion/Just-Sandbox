using Feature.Player.Domain;
using Feature.UI.Presentation;
using Shared.Data;
using System;
using Zenject;

namespace Feature.Player.Presentation
{
    /// <summary>
    /// Manages the player's health state and visual representation, updating the UI and triggering animations in
    /// response to player life events such as taking damage, death, and respawn.
    /// </summary>
    /// <remarks>The Presenter class subscribes to player life events to keep the health display and related
    /// animations in sync with the player's state. It is responsible for initializing the health UI, handling
    /// transitions for damage, death, and respawn, and cleaning up event subscriptions when disposed. This class is
    /// intended to be used as part of the player's presentation layer and should be initialized and disposed according
    /// to the player's lifecycle.</remarks>
    public class LifePresenter : IInitializable, IDisposable
    {
        private readonly IReadOnlyPlayer _playerState;
        private readonly ILifeEvents _events;
        private readonly IAnimator _animator;
        private readonly ILifeView _view;

        public LifePresenter(
            IReadOnlyPlayer playerState,
            ILifeEvents events,
            IAnimator healthAnimator,
            ILifeView view)
        {
            _playerState = playerState;
            _events = events;
            _animator = healthAnimator;
            _view = view;
        }

        public void Initialize()
        {
            AnimateInitialHealth();
            _events.ReceivedDamage += OnHealthChanged;
            _events.PlayerDied += OnPlayerDied;
            _events.PlayerRespawned += OnPlayerRespawned;
        }

        public void Dispose()
        {
            _events.ReceivedDamage -= OnHealthChanged;
            _events.PlayerDied -= OnPlayerDied;
            _events.PlayerRespawned -= OnPlayerRespawned;
        }

        private void OnHealthChanged(AttackInfo attack)
        {
            float percent = _playerState.CurrentHealth / _playerState.MaxHealth;
            _animator.Animate(_view.CurrentFillAmount(), percent, value => _view.SetHealth(value));
        }

        private void OnPlayerDied()
        {
            float deathDuration = 4f;
            _animator.Animate(0f, 1f, value => _view.SetDeathScreenTransparency(value), deathDuration);
        }

        private void OnPlayerRespawned()
        {
            float respawnDuration = 0.2f;
            _view.SetHealth(_playerState.CurrentHealth / _playerState.MaxHealth);
            _animator.Animate(1f, 0f, value => _view.SetDeathScreenTransparency(value), respawnDuration);
        }

        private void AnimateInitialHealth(float duration = 0.5f)
        {
            float targetPercent = _playerState.CurrentHealth / _playerState.MaxHealth;
            _animator.Animate(0f, targetPercent, value => _view.SetHealth(value), duration);
        }

    }
}