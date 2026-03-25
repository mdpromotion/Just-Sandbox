using Feature.Combat.Application;
using Feature.Combat.Domain;
using Feature.Combat.Infrastructure;
using Feature.Items.Infrastructure;
using Feature.Toolbox.Infrastructure;
using Moq;
using NUnit.Framework;
using Shared.Data;
using Shared.Domain;
using Shared.Providers;
using System.Numerics;

namespace Tests.Combat
{
    [TestFixture]
    public class WeaponShotUseCaseTests
    {
        private Mock<IWeaponService> _weaponService;
        private Mock<IItemConfigService> _configService;
        private Mock<IPlayerTransformData> _player;

        private WeaponShotUseCase _sut;

        [SetUp]
        public void Setup()
        {
            _weaponService = new Mock<IWeaponService>();
            _configService = new Mock<IItemConfigService>();
            _player = new Mock<IPlayerTransformData>();

            _player.Setup(p => p.Position).Returns(new Position3(0, 0, 0));

            _sut = new WeaponShotUseCase(_weaponService.Object, _configService.Object, _player.Object);
        }

        [Test]
        public void Shoot_ReturnsFailure_WhenWeaponConfigNotFound()
        {
            var weapon = new Mock<IWeapon>();
            weapon.Setup(w => w.ConfigId).Returns(1);

            _configService.Setup(c => c.GetItemConfig(1)).Returns(Result<IItemProvider>.Failure("Not found"));

            var result = _sut.Shoot(weapon.Object);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Weapon config not found.", result.Error);
        }

        [Test]
        public void Shoot_ReturnsFailure_WhenNoTarget()
        {
            var weapon = new Mock<IWeapon>();
            weapon.Setup(w => w.ConfigId).Returns(2);

            var weaponConfig = new Mock<IWeaponProvider>();
            _configService.Setup(c => c.GetItemConfig(2)).Returns(Result<IItemProvider>.Success(weaponConfig.Object));

            _weaponService.Setup(ws => ws.GetTarget(It.IsAny<float>())).Returns((ITarget)null);

            var result = _sut.Shoot(weapon.Object);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No targets.", result.Error);
        }

        [Test]
        public void Shoot_ReturnsFailure_WhenItemIsNotWeapon()
        {
            var weapon = new Mock<IWeapon>();
            weapon.Setup(w => w.ConfigId).Returns(3);

            var itemConfig = new Mock<IItemProvider>();
            _configService.Setup(c => c.GetItemConfig(3)).Returns(Result<IItemProvider>.Success(itemConfig.Object));

            _weaponService.Setup(ws => ws.GetTarget(It.IsAny<float>())).Returns(Mock.Of<ITarget>());

            var result = _sut.Shoot(weapon.Object);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Current item is not a weapon.", result.Error);
        }

        [Test]
        public void Shoot_ReturnsFailure_WhenDamageApplicationFails()
        {
            var weapon = new Mock<IWeapon>();
            weapon.Setup(w => w.ConfigId).Returns(4);

            var weaponConfig = new Mock<IWeaponProvider>();
            weaponConfig.SetupGet(w => w.Damage).Returns(10);
            weaponConfig.SetupGet(w => w.Knockback).Returns(5);

            _configService.Setup(c => c.GetItemConfig(4)).Returns(Result<IItemProvider>.Success(weaponConfig.Object));

            var target = new Mock<ITarget>();
            target.Setup(t => t.ReceiveDamage(It.IsAny<AttackInfo>())).Returns(Result.Failure("Cannot damage"));

            _weaponService.Setup(ws => ws.GetTarget(It.IsAny<float>())).Returns(target.Object);

            var result = _sut.Shoot(weapon.Object);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Failed to apply damage to target.", result.Error);
        }

        [Test]
        public void Shoot_ReturnsSuccess_WhenEverythingSucceeds()
        {
            var weapon = new Mock<IWeapon>();
            weapon.Setup(w => w.ConfigId).Returns(5);

            var weaponConfig = new Mock<IWeaponProvider>();
            weaponConfig.SetupGet(w => w.Damage).Returns(10);
            weaponConfig.SetupGet(w => w.Knockback).Returns(5);

            _configService.Setup(c => c.GetItemConfig(5)).Returns(Result<IItemProvider>.Success(weaponConfig.Object));

            var target = new Mock<ITarget>();
            target.Setup(t => t.ReceiveDamage(It.IsAny<AttackInfo>())).Returns(Result.Success());

            _weaponService.Setup(ws => ws.GetTarget(It.IsAny<float>())).Returns(target.Object);

            var result = _sut.Shoot(weapon.Object);

            Assert.IsTrue(result.IsSuccess);
        }
    }
}