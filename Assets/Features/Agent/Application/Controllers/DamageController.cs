using Feature.Agent.Infrastructure;
using Feature.Player.Data;
using Shared.Data;
using Shared.Domain;

namespace Feature.Agent.Application
{
    /// <summary>
    /// Controls damage interactions for an attacker, managing target detection and executing attacks based on the
    /// attacker's state and position.
    /// </summary>
    /// <remarks>This class listens for target entry and exit events to manage the current target. It requires
    /// an attacker agent, an attack use case, a trigger handler for target events, and a navigation mesh controller to
    /// function correctly. The Tick method should be called regularly to process attacks on the current target if the
    /// attacker is alive.</remarks>
    public class DamageController : IDamageController
    {
        private readonly Domain.Agent _attacker;
        private readonly IAttackUseCase _attackUseCase;
        private readonly ITriggerHandler _triggerHandler;
        private readonly INavMeshController _controller;

        private ITarget _currentTarget;
        private Position3 _position => _controller.Position;

        public DamageController(Domain.Agent attacker, IAttackUseCase attackUseCase, ITriggerHandler triggerHandler, INavMeshController controller)
        {
            _attacker = attacker;
            _attackUseCase = attackUseCase;
            _triggerHandler = triggerHandler;
            _controller = controller;

            _triggerHandler.TargetEntered += OnTargetEntered;
            _triggerHandler.TargetExited += OnTargetExited;
        }

        public void OnTargetEntered(ITarget target)
        {
            _currentTarget = target;
        }

        public void OnTargetExited(ITarget target)
        {
            if (_currentTarget == target)
            {
                _currentTarget = null;
            }
        }

        public void Tick()
        {
            if (_currentTarget == null) return;
            if (!_attacker.IsAlive) return;

            AttackData attackData = new(
                _attacker,
                _attacker.Damage,
                _attacker.AttackSpeed,
                _currentTarget,
                _position);

            _attackUseCase.Attack(attackData);
        }
    }
}