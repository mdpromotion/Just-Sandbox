using Feature.Combat.Application;
using Feature.Combat.Data;
using Feature.Combat.Domain;
using Feature.Inventory.Domain;
using Moq;
using NUnit.Framework;
using Shared.Providers;
using System;

namespace Tests.Combat
{
    [TestFixture]
    public class UseItemUseCaseTests
    {
        private Mock<IReadOnlyWeaponInventory> _weaponInventory;
        private Mock<IReadOnlyPlayerInventory> _playerInventory;
        private Mock<ICooldownService> _cooldown;
        private Mock<ITimeProvider> _timeProvider;

        private UseItemUseCase _sut;

        private class TestWeapon : IWeapon, IUsable, IReloadable
        {
            public Guid Id { get; set; }
            public int ConfigId { get; set; }
            public Guid WorldId { get; set; }
            public WeaponType Type { get; set; }
            public int CurrentAmmo { get; set; }
            public int ReserveAmmo { get; set; }
            public float Cooldown { get; set; } = 1f;
            public float ReloadCooldown { get; set; } = 2f;

            public Result Use()
            {
                return Result.Success();
            }

            public Result Reload()
            {
                return Result.Success();
            }
        }

        private TestWeapon CreateWeapon(Guid? id = null, Guid? worldId = null)
        {
            return new TestWeapon
            {
                Id = id ?? Guid.NewGuid(),
                WorldId = worldId ?? Guid.NewGuid(),
                ConfigId = 1,
                Type = WeaponType.Shootable,
                CurrentAmmo = 10,
                ReserveAmmo = 30
            };
        }

        [SetUp]
        public void Setup()
        {
            _weaponInventory = new Mock<IReadOnlyWeaponInventory>();
            _playerInventory = new Mock<IReadOnlyPlayerInventory>();
            _cooldown = new Mock<ICooldownService>();
            _timeProvider = new Mock<ITimeProvider>();

            _sut = new UseItemUseCase(
                _weaponInventory.Object,
                _playerInventory.Object,
                _cooldown.Object,
                _timeProvider.Object);
        }

        [Test]
        public void Use_ReturnsFailure_WhenNoItemSelected()
        {
            _playerInventory.Setup(p => p.GetSelectedWorldId()).Returns(Guid.Empty);

            var result = _sut.Use();

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Cannot find any weapon in player's hand", result.Error);
        }

        [Test]
        public void Use_ReturnsFailure_WhenWeaponNotFound()
        {
            var worldId = Guid.NewGuid();
            _playerInventory.Setup(p => p.GetSelectedWorldId()).Returns(worldId);
            _weaponInventory.Setup(w => w.GetByWorldId(worldId)).Returns((IWeapon)null);

            var result = _sut.Use();

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Cannot find any weapon in player's hand", result.Error);
        }

        [Test]
        public void Use_ReturnsFailure_WhenCooldownNotAvailable()
        {
            var weapon = CreateWeapon();
            _playerInventory.Setup(p => p.GetSelectedWorldId()).Returns(weapon.WorldId);
            _weaponInventory.Setup(w => w.GetByWorldId(weapon.WorldId)).Returns(weapon);
            _cooldown.Setup(c => c.IsAvaliable(It.IsAny<float>())).Returns(false);

            var result = _sut.Use();

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Cooldown", result.Error);
        }


        [Test]
        public void Use_ReturnsSuccess_WhenUsableAndCooldownAvailable()
        {
            var weapon = CreateWeapon();
            _playerInventory.Setup(p => p.GetSelectedWorldId()).Returns(weapon.WorldId);
            _weaponInventory.Setup(w => w.GetByWorldId(weapon.WorldId)).Returns(weapon);
            _timeProvider.Setup(t => t.Now).Returns(5f);
            _cooldown.Setup(c => c.IsAvaliable(5f)).Returns(true);

            var result = _sut.Use();

            Assert.IsTrue(result.IsSuccess);
            _cooldown.Verify(c => c.UpdateLastUseTime(5f, weapon.Cooldown), Times.Once);
        }

        [Test]
        public void Reload_ReturnsFailure_WhenCooldownNotAvailable()
        {
            var weapon = CreateWeapon();
            _playerInventory.Setup(p => p.GetSelectedWorldId()).Returns(weapon.WorldId);
            _weaponInventory.Setup(w => w.GetByWorldId(weapon.WorldId)).Returns(weapon);
            _cooldown.Setup(c => c.IsAvaliable(It.IsAny<float>())).Returns(false);

            var result = _sut.Reload();

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Cooldown", result.Error);
        }


        [Test]
        public void Reload_ReturnsSuccess_WhenReloadableAndCooldownAvailable()
        {
            var weapon = CreateWeapon();
            _playerInventory.Setup(p => p.GetSelectedWorldId()).Returns(weapon.WorldId);
            _weaponInventory.Setup(w => w.GetByWorldId(weapon.WorldId)).Returns(weapon);
            _timeProvider.Setup(t => t.Now).Returns(10f);
            _cooldown.Setup(c => c.IsAvaliable(10f)).Returns(true);

            var result = _sut.Reload();

            Assert.IsTrue(result.IsSuccess);
            _cooldown.Verify(c => c.UpdateLastUseTime(10f, weapon.ReloadCooldown), Times.Once);
        }
    }
}