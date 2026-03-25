using Core.Data;
using Feature.Combat.Application;
using Feature.Combat.Domain;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Combat
{
    [TestFixture]
    public class UseItemOrchestratorTests
    {
        private Mock<IUseItemUseCase> _useItem;
        private Mock<IWeaponShotUseCase> _useWeapon;
        private Mock<IReadOnlyCoreGameStates> _gameStates;
        private Mock<ILogger> _logger;

        private UseItemOrchestrator _sut;

        [SetUp]
        public void Setup()
        {
            _useItem = new Mock<IUseItemUseCase>();
            _useWeapon = new Mock<IWeaponShotUseCase>();
            _gameStates = new Mock<IReadOnlyCoreGameStates>();
            _logger = new Mock<ILogger>();

            _sut = new UseItemOrchestrator(
                _useItem.Object,
                _useWeapon.Object,
                _gameStates.Object,
                _logger.Object);
        }

        [Test]
        public void Use_DoesNothing_WhenPlayerNotControllable()
        {
            _gameStates.Setup(gs => gs.IsPlayerControllable).Returns(false);

            _sut.Use();

            _useItem.Verify(u => u.Use(), Times.Never);
            _useWeapon.Verify(w => w.Shoot(It.IsAny<IWeapon>()), Times.Never);
        }

        [Test]
        public void Use_LogsWarning_WhenUseItemFails()
        {
            _gameStates.Setup(gs => gs.IsPlayerControllable).Returns(true);

            _useItem.Setup(u => u.Use())
                .Returns(Result<IWeapon>.Failure("fail"));

            _sut.Use();

            _logger.Verify(l => l.LogWarning(UseItemOrchestrator.LogTag, "fail"), Times.Once);
            _useWeapon.Verify(w => w.Shoot(It.IsAny<IWeapon>()), Times.Never);
        }

        [Test]
        public void Use_InvokesUsedEvent_WhenUseItemSuccess()
        {
            _gameStates.Setup(gs => gs.IsPlayerControllable).Returns(true);

            var weapon = new Mock<IWeapon>().Object;

            _useItem.Setup(u => u.Use())
                .Returns(Result<IWeapon>.Success(weapon));

            _useWeapon.Setup(w => w.Shoot(It.IsAny<IWeapon>()))
                .Returns(Result.Success());

            IWeapon invokedWeapon = null;
            _sut.Used += w => invokedWeapon = w;

            _sut.Use();

            Assert.AreEqual(weapon, invokedWeapon);
        }

        [Test]
        public void Use_LogsWarning_WhenShootFails()
        {
            _gameStates.Setup(gs => gs.IsPlayerControllable).Returns(true);

            var weapon = new Mock<IWeapon>().Object;

            _useItem.Setup(u => u.Use())
                .Returns(Result<IWeapon>.Success(weapon));

            _useWeapon.Setup(w => w.Shoot(It.IsAny<IWeapon>()))
                .Returns(Result.Failure("shoot fail"));

            _sut.Use();

            _logger.Verify(l => l.LogWarning(UseItemOrchestrator.LogTag, "shoot fail"), Times.Once);
        }

        [Test]
        public void Use_CallsShoot_WhenUseItemSuccess()
        {
            _gameStates.Setup(gs => gs.IsPlayerControllable).Returns(true);

            var weapon = new Mock<IWeapon>().Object;

            _useItem.Setup(u => u.Use())
                .Returns(Result<IWeapon>.Success(weapon));

            _useWeapon.Setup(w => w.Shoot(It.IsAny<IWeapon>()))
                .Returns(Result.Success());

            _sut.Use();

            _useWeapon.Verify(w => w.Shoot(weapon), Times.Once);
        }

        [Test]
        public void Reload_DoesNothing_WhenPlayerNotControllable()
        {
            _gameStates.Setup(gs => gs.IsPlayerControllable).Returns(false);

            _sut.Reload();

            _useItem.Verify(u => u.Reload(), Times.Never);
        }

        [Test]
        public void Reload_LogsWarning_WhenReloadFails()
        {
            _gameStates.Setup(gs => gs.IsPlayerControllable).Returns(true);

            _useItem.Setup(u => u.Reload())
                .Returns(Result<IWeapon>.Failure("reload fail"));

            _sut.Reload();

            _logger.Verify(l => l.LogWarning(UseItemOrchestrator.LogTag, "reload fail"), Times.Once);
        }

        [Test]
        public void Reload_InvokesReloadedEvent_WhenSuccess()
        {
            _gameStates.Setup(gs => gs.IsPlayerControllable).Returns(true);

            var weapon = new Mock<IWeapon>().Object;

            _useItem.Setup(u => u.Reload())
                .Returns(Result<IWeapon>.Success(weapon));

            IWeapon invokedWeapon = null;
            _sut.Reloaded += w => invokedWeapon = w;

            _sut.Reload();

            Assert.AreEqual(weapon, invokedWeapon);
        }
    }
}