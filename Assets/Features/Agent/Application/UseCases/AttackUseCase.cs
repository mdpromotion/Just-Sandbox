using Feature.Agent.Domain;
using Feature.Player.Data;
using Shared.Data;
using Shared.Domain;
using Shared.Providers;
using UnityEngine;

namespace Feature.Agent.Application
{
    /// <summary>
    /// Handles the execution of attack actions within the game, managing cooldowns and game state.
    /// </summary>
    /// <remarks>This class requires an instance of IReadOnlyGameState to check the current game state,
    /// ITimeProvider to manage time-related functionality, ICooldownService to handle attack cooldowns, and ILogger for
    /// logging actions and warnings. The Attack method will not execute if the game is paused or if the attack is on
    /// cooldown.</remarks>
    public class AttackUseCase : IAttackUseCase
    {
        private readonly static string LogTag = nameof(AttackUseCase);

        private readonly IReadOnlyGameState _gameState;
        private readonly ITimeProvider _time;
        private readonly ICooldownService _cooldown;
        private readonly ILogger _logger;

        private readonly float _baseCooldown = 3f;

        public AttackUseCase(
            IReadOnlyGameState gameState,
            ITimeProvider time,
            ICooldownService cooldown,
            ILogger logger)
        {
            _gameState = gameState;
            _time = time;
            _cooldown = cooldown;
            _logger = logger;
        }

        public void Attack(AttackData data)
        {
            if (!_cooldown.CanAttack(data.Attacker.Id, _time.Now))
                return;

            if (_gameState.IsPaused)
            {
                _logger.LogWarning(LogTag, "Game is paused. Attack cannot be executed.");
                return;
            }

            _logger.Log("Executing attack...");

            float knockback = data.Damage / 1.5f;

            var result = data.Target.ReceiveDamage(new AttackInfo(data.Damage, knockback, data.AttackerPosition));
            if (!result.IsSuccess)
            {
                _logger.LogWarning(LogTag, $"Attack failed: {result.Error}");
                return;
            }

            float cooldown = _baseCooldown / data.AttackSpeed;

            _cooldown.UpdateAttackTime(data.Attacker.Id, _time.Now, cooldown);
        }
    }
}