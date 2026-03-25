using Core.Data;
using Feature.Player.Application;
using Moq;
using NUnit.Framework;
using Shared.Data;
using Shared.Providers;
using UnityEngine;

namespace Tests.Player
{
    [TestFixture]
    public class MovementUseCaseTests
    {
        private Mock<IMovementCalculator> _calculator;
        private Mock<IReadOnlyCoreGameStates> _coreState;
        private Mock<IPlayerTransformData> _transformProvider;
        private Mock<IPhysicsController> _playerController;
        private Mock<IReadOnlyMovementInputState> _movementState;
        private Mock<IReadOnlyPlayerWorldState> _playerState;
        private Mock<ILogger> _logger;

        private MovementUseCase _sut;

        [SetUp]
        public void Setup()
        {
            _calculator = new Mock<IMovementCalculator>();
            _coreState = new Mock<IReadOnlyCoreGameStates>();
            _transformProvider = new Mock<IPlayerTransformData>();
            _playerController = new Mock<IPhysicsController>();
            _movementState = new Mock<IReadOnlyMovementInputState>();
            _playerState = new Mock<IReadOnlyPlayerWorldState>();
            _logger = new Mock<ILogger>();

            _sut = new MovementUseCase(
                _calculator.Object,
                _coreState.Object,
                _transformProvider.Object,
                _playerController.Object,
                _movementState.Object,
                _playerState.Object,
                _logger.Object);
        }

        [Test]
        public void Move_SetsKinematic_WhenGamePaused()
        {
            _coreState.Setup(c => c.Game.IsPaused).Returns(true);

            _sut.Move();

            _playerController.Verify(p => p.SwitchKinematicState(true), Times.Once);
            _playerController.Verify(p => p.Move(It.IsAny<Position3>()), Times.Never);
        }

        [Test]
        public void Move_DoesNothing_WhenPlayerNotControllable()
        {
            _coreState.Setup(c => c.Game.IsPaused).Returns(false);
            _coreState.Setup(c => c.IsPlayerControllable).Returns(false);

            _sut.Move();

            _playerController.Verify(p => p.Move(It.IsAny<Position3>()), Times.Never);
        }

        [Test]
        public void Move_CallsMoveWithCalculatedVelocity_WhenControllable()
        {
            _coreState.Setup(c => c.Game.IsPaused).Returns(false);
            _coreState.Setup(c => c.IsPlayerControllable).Returns(true);

            _movementState.SetupGet(m => m.InputDirection).Returns(new Position2(1, 0));
            _movementState.SetupGet(m => m.IsJumping).Returns(true);
            _playerState.SetupGet(p => p.IsGrounded).Returns(true);
            _playerController.SetupGet(p => p.CurrentVelocity).Returns(new Position3(0, 0, 0));

            var expectedVelocity = new Position3(1, 2, 3);
            _calculator.Setup(c => c.CalculateGroundVelocity(
                It.IsAny<Position2>(),
                _transformProvider.Object,
                It.IsAny<float>(),
                It.IsAny<float>(),
                It.IsAny<float>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(expectedVelocity);

            _playerController.Setup(p => p.Move(expectedVelocity))
                .Returns(Result.Success());

            _sut.Move();

            _playerController.Verify(p => p.SwitchKinematicState(false), Times.Once);
            _playerController.Verify(p => p.Move(expectedVelocity), Times.Once);
        }

        [Test]
        public void Move_LogsWarning_WhenMoveFails()
        {
            _coreState.Setup(c => c.Game.IsPaused).Returns(false);
            _coreState.Setup(c => c.IsPlayerControllable).Returns(true);

            var expectedVelocity = new Position3(1, 0, 0);
            _calculator.Setup(c => c.CalculateGroundVelocity(
                It.IsAny<Position2>(),
                _transformProvider.Object,
                It.IsAny<float>(),
                It.IsAny<float>(),
                It.IsAny<float>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(expectedVelocity);

            _playerController.Setup(p => p.Move(expectedVelocity))
                .Returns(Result.Failure("collision"));

            _sut.Move();

            _logger.Verify(l => l.LogWarning(It.IsAny<string>(), "Failed to move player: collision"), Times.Once);
        }
    }
}