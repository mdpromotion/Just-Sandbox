using Feature.Combat.Data;
using Feature.Combat.Domain;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests.Combat
{
    [TestFixture]
    public class WeaponInventoryTests
    {
        private WeaponInventory _sut;

        private class TestWeapon : IWeapon
        {
            public Guid Id { get; set; }
            public int ConfigId { get; set; }
            public Guid WorldId { get; set; }
            public WeaponType Type { get; set; }
            public int CurrentAmmo { get; set; }
            public int ReserveAmmo { get; set; }
        }

        private TestWeapon CreateWeapon(
            Guid? id = null,
            Guid? worldId = null,
            int configId = 1,
            WeaponType type = WeaponType.Shootable,
            int currentAmmo = 10,
            int reserveAmmo = 30)
        {
            return new TestWeapon
            {
                Id = id ?? Guid.NewGuid(),
                WorldId = worldId ?? Guid.NewGuid(),
                ConfigId = configId,
                Type = type,
                CurrentAmmo = currentAmmo,
                ReserveAmmo = reserveAmmo
            };
        }

        [SetUp]
        public void Setup()
        {
            _sut = new WeaponInventory();
        }

        [Test]
        public void Add_ReturnsSuccess_WhenWeaponIsNew()
        {
            var weapon = CreateWeapon();

            var result = _sut.Add(weapon);

            Assert.IsTrue(result.IsSuccess);
            Assert.Contains(weapon, _sut.Weapons.ToList());
        }

        [Test]
        public void Add_ReturnsFailure_WhenWeaponAlreadyExists()
        {
            var weapon = CreateWeapon();

            _sut.Add(weapon);
            var result = _sut.Add(weapon);

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void Delete_RemovesWeapon_FromInventory()
        {
            var weapon = CreateWeapon();

            _sut.Add(weapon);

            _sut.Delete(weapon);

            Assert.IsFalse(_sut.Weapons.Contains(weapon));
        }

        [Test]
        public void Delete_DoesNothing_WhenWeaponNotExists()
        {
            var weapon = CreateWeapon();

            _sut.Delete(weapon);

            Assert.IsEmpty(_sut.Weapons);
        }

        [Test]
        public void GetById_ReturnsWeapon_WhenExists()
        {
            var weapon = CreateWeapon();

            _sut.Add(weapon);

            var result = _sut.GetById(weapon.Id);

            Assert.AreEqual(weapon, result);
        }

        [Test]
        public void GetById_ReturnsNull_WhenNotFound()
        {
            var result = _sut.GetById(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public void GetByWorldId_ReturnsWeapon_WhenExists()
        {
            var weapon = CreateWeapon();

            _sut.Add(weapon);

            var result = _sut.GetByWorldId(weapon.WorldId);

            Assert.AreEqual(weapon, result);
        }

        [Test]
        public void GetByWorldId_ReturnsNull_WhenNotFound()
        {
            var result = _sut.GetByWorldId(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public void Weapons_IsReadOnly()
        {
            var weapon = CreateWeapon();

            _sut.Add(weapon);

            Assert.Throws<NotSupportedException>(() =>
            {
                ((System.Collections.Generic.IList<IWeapon>)_sut.Weapons).Add(CreateWeapon());
            });
        }
    }
}