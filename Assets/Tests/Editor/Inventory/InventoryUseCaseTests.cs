using Feature.Inventory.Application;
using Feature.Inventory.Domain;
using Moq;
using NUnit.Framework;
using Shared.Data;
using Shared.Providers;
using System;
using UnityEngine;

namespace Tests.Inventory
{
    [TestFixture]
    public class InventoryUseCaseTests
    {
        private Mock<IPlayerInventory> _playerInventory;
        private Mock<ITimeProvider> _timeProvider;
        private Mock<ICooldownService> _cooldownService;

        private IManagementUseCase _sut;

        [SetUp]
        public void Setup()
        {
            _playerInventory = new Mock<IPlayerInventory>();
            _timeProvider = new Mock<ITimeProvider>();
            _cooldownService = new Mock<ICooldownService>();
            _sut = new ManagementUseCase(_playerInventory.Object, _timeProvider.Object, _cooldownService.Object);
        }

        [Test]
        public void TryAddItem_ReturnsSuccess_WhenInventoryAddIsSuccessful()
        {
            var item = new ItemContext(new GameObject(), 0, Guid.NewGuid());

            _playerInventory.Setup(pi => pi.Add(It.IsAny<int>(), It.IsAny<Guid>(), It.IsAny<int?>()))
                .Returns(Result<InventoryItem>.Success(new InventoryItem(Guid.NewGuid(), 0, 0, Guid.NewGuid())));

            _cooldownService.Setup(cs => cs.IsAvaliable(It.IsAny<float>()));

            _timeProvider.Setup(tp => tp.Now).Returns(1);

            _cooldownService.Setup(cs => cs.UpdateLastUseTime(It.IsAny<float>(), It.IsAny<float>()));

            var result = _sut.TryAddItem(item);
            Assert.IsTrue(result.IsSuccess);
        }
        [Test]
        public void TryAddItem_ReturnsFailure_WhenInventoryAddFails()
        {
            var item = new ItemContext(new GameObject(), 0, Guid.NewGuid());

            _playerInventory.Setup(pi => pi.Add(It.IsAny<int>(), It.IsAny<Guid>(), It.IsAny<int?>()))
                .Returns(Result<InventoryItem>.Failure("error"));

            var result = _sut.TryAddItem(item);

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void TrySelectItem_ReturnsFailure_WhenSelectSlotFails()
        {
            _playerInventory.SetupGet(pi => pi.CurrentSlot).Returns(0);

            _playerInventory.Setup(pi => pi.SelectSlot(It.IsAny<int>()))
                .Returns(Result.Failure("error"));

            var result = _sut.TrySelectItem(1, 0.5f);

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void TrySelectItem_ReturnsSuccess_AndCreatesCorrectSnapshot()
        {
            var previousItem = new InventoryItem(Guid.NewGuid(), 1, 0, Guid.NewGuid());
            var currentItem = new InventoryItem(Guid.NewGuid(), 2, 1, Guid.NewGuid());

            _playerInventory.SetupGet(pi => pi.CurrentSlot).Returns(0);

            _playerInventory.Setup(pi => pi.SelectSlot(It.IsAny<int>()))
                .Returns(Result.Success());

            _playerInventory.Setup(pi => pi.GetBySlot(0)).Returns(previousItem);
            _playerInventory.Setup(pi => pi.GetSelectedItem()).Returns(currentItem);

            _timeProvider.Setup(tp => tp.Now).Returns(10f);

            var result = _sut.TrySelectItem(1, 2f);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(previousItem.WorldId, result.Value.PreviousWorldId);
            Assert.AreEqual(currentItem.WorldId, result.Value.CurrentWorldId);
            Assert.AreEqual(0, result.Value.PreviousSlotId);
            Assert.AreEqual(1, result.Value.CurrentSlotId);
        }

        [Test]
        public void TrySelectItem_UpdatesCooldown()
        {
            _playerInventory.SetupGet(pi => pi.CurrentSlot).Returns(0);

            _playerInventory.Setup(pi => pi.SelectSlot(It.IsAny<int>()))
                .Returns(Result.Success());

            _playerInventory.Setup(pi => pi.GetBySlot(It.IsAny<int>())).Returns((InventoryItem)null);
            _playerInventory.Setup(pi => pi.GetSelectedItem()).Returns((InventoryItem)null);

            _timeProvider.Setup(tp => tp.Now).Returns(5f);

            _sut.TrySelectItem(1, 3f);

            _cooldownService.Verify(cs => cs.UpdateLastUseTime(5f, 3f), Times.Once);
        }

        [Test]
        public void TryDeleteItem_ReturnsFailure_WhenNoItemSelected()
        {
            _playerInventory.Setup(pi => pi.GetSelectedItem()).Returns((InventoryItem)null);

            var result = _sut.TryDeleteItem();

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void TryDeleteItem_ReturnsSuccess_AndDeletesItem()
        {
            var item = new InventoryItem(Guid.NewGuid(), 1, 0, Guid.NewGuid());

            _playerInventory.Setup(pi => pi.GetSelectedItem()).Returns(item);

            var result = _sut.TryDeleteItem();

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(item, result.Value);

            _playerInventory.Verify(pi => pi.Delete(item), Times.Once);
        }
    }
}