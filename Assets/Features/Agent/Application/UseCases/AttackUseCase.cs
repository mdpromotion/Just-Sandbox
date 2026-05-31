using Feature.Agent.Application;
using Feature.Player.Data;
using Features.Agent.Application.Interfaces;
using Features.Agent.Infrastructure.Assembler.Interfaces;
using Features.Core.Interfaces;
using Shared.Data;
using Shared.Providers;
using UnityEngine;

namespace Features.Agent.Application.UseCases
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
        private const string LogTag = nameof(AttackUseCase);

        private readonly IReadOnlyGameState _gameState;
        private readonly ITimeProvider _time;
        private readonly ICooldownService _cooldown;
        private readonly ILogger _logger;

        private const float BaseCooldown = 3f;

        protected AttackUseCase(
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

            float cooldown = BaseCooldown / data.AttackSpeed;

            _cooldown.UpdateAttackTime(data.Attacker.Id, _time.Now, cooldown);
        }
    }
}