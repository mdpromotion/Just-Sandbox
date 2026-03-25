using Core.Data;
using Feature.Player.Application;
using Moq;
using NUnit.Framework;
using Shared.Data;
using Shared.Providers;
using System;
using UnityEngine;

namespace Tests.Player
{
    [TestFixture]
    public class LifeUseCaseTests
    {
        private Mock<IPunchCalculator> _calculator;
        private Mock<IReadOnlyCoreGameStates> _gameState;
        private Mock<IPlayerTransformController> _playerTransformController;
        private Mock<IPlayerTransformData> _transformData;
        private Mock<ILogger> _logger;
        private Mock<IPhysicsController> _physicsController;

        private LifeUseCase _sut;

        [SetUp]
        public void Setup()
        {
            _calculator = new Mock<IPunchCalculator>();
            _gameState = new Mock<IReadOnlyCoreGameStates>();
            _playerTransformController = new Mock<IPlayerTransformController>();
            _transformData = new Mock<IPlayerTransformData>();
            _logger = new Mock<ILogger>();
            _physicsController = new Mock<IPhysicsController>();

            _transformData.SetupGet(t => t.Position).Returns(new Position3(0, 0, 0));

            _sut = new LifeUseCase(
                _calculator.Object,
                _gameState.Object,
                _playerTransformController.Object,
                _transformData.Object,
                _logger.Object);
        }

        [Test]
        public void OnPlayerDamaged_DoesNothing_WhenPlayerNotControllable()
        {
            _gameState.Setup(g => g.IsPlayerControllable).Returns(false);
            var attack = new AttackInfo(10, 5, new Position3(1, 0, 0));

            _sut.OnPlayerDamaged(_physicsController.Object, attack);

            _logger.Verify(l => l.LogWarning(It.IsAny<string>(), "Player is not controllable and cannot receive damage."), Times.Once);
            _physicsController.Verify(p => p.Punch(It.IsAny<Position3>(), It.IsAny<float>()), Times.Never);
        }

        [Test]
        public void OnPlayerDamaged_CallsPunch_WhenControllable()
        {
            _gameState.Setup(g => g.IsPlayerControllable).Returns(true);
            var attack = new AttackInfo(10, 5, new Position3(1, 0, 0));
            var punchVelocity = new Position3(0, 1, 0);

            _calculator.Setup(c => c.CalculatePunchVelocity(attack.AttackerPosition, _transformData.Object.Position, attack.Knockback))
                .Returns(punchVelocity);

            _physicsController.Setup(p => p.Punch(punchVelocity, It.IsAny<float>())).Returns(Result.Success());

            _sut.OnPlayerDamaged(_physicsController.Object, attack);

            _physicsController.Verify(p => p.Punch(punchVelocity, It.IsAny<float>()), Times.Once);
        }

        [Test]
        public void OnPlayerDamaged_LogsWarning_WhenPunchFails()
        {
            _gameState.Setup(g => g.IsPlayerControllable).Returns(true);
            var attack = new AttackInfo(10, 5, new Position3(0, 0, 0));
            var punchVelocity = new Position3(0, 1, 0);

            _calculator.Setup(c => c.CalculatePunchVelocity(attack.AttackerPosition, _transformData.Object.Position, attack.Knockback))
                .Returns(punchVelocity);

            _physicsController.Setup(p => p.Punch(punchVelocity, It.IsAny<float>())).Returns(Result.Failure("collision"));

            _sut.OnPlayerDamaged(_physicsController.Object, attack);

            _logger.Verify(l => l.LogWarning(It.IsAny<string>(), "collision"), Times.Once);
        }

        [Test]
        public void OnPlayerDied_TogglesConstraintsOff()
        {
            _sut.OnPlayerDied(_physicsController.Object);

            _physicsController.Verify(p => p.ToggleConstraints(false), Times.Once);
        }

        [Test]
        public void OnPlayerRespawned_CallsAllResetMethods()
        {
            _sut.OnPlayerRespawned(_physicsController.Object);

            _playerTransformController.Verify(p => p.Teleport(Position3.Zero), Times.Once);
            _playerTransformController.Verify(p => p.ResetAngle(), Times.Once);
            _physicsController.Verify(p => p.SwitchKinematicState(false), Times.Once);
            _physicsController.Verify(p => p.ToggleConstraints(true), Times.Once);
            _physicsController.Verify(p => p.ResetVelocity(), Times.Once);
        }

        [Test]
        public void OnPlayerRespawned_LogsError_WhenExceptionThrown()
        {
            _playerTransformController.Setup(p => p.Teleport(It.IsAny<Position3>())).Throws(new Exception("fail"));

            _sut.OnPlayerRespawned(_physicsController.Object);

            _logger.Verify(l => l.LogError(It.IsAny<string>(), "Respawn failed"), Times.Once);
            _logger.Verify(l => l.LogException(It.IsAny<Exception>()), Times.Once);
        }
    }
}