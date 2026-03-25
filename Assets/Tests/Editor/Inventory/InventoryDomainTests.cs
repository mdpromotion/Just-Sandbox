using Feature.Inventory.Domain;
using NUnit.Framework;
using System;

namespace Tests.Inventory
{
    [TestFixture]
    public class InventoryDomainTests
    {
        private PlayerInventory _inventory;

        [SetUp]
        public void Setup()
        {
            _inventory = new PlayerInventory(maxSlots: 7);
        }

        [Test]
        public void TryAddItem_ReturnsSuccess_WhenInventoryHasSpace()
        {
            var result = _inventory.Add(configId: 1, worldId: Guid.NewGuid());
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, _inventory.Items.Count);
        }

        [Test]
        public void TryAddItem_ReturnsFailure_WhenInventoryIsFull()
        {
            for (int i = 0; i < 7; i++)
            {
                var result = _inventory.Add(configId: i, worldId: Guid.NewGuid());
                Assert.IsTrue(result.IsSuccess);
            }

            var overflowResult = _inventory.Add(configId: 999, worldId: Guid.NewGuid());

            Assert.IsFalse(overflowResult.IsSuccess);
            Assert.AreEqual("Inventory is full.", overflowResult.Error);
        }

        [Test]
        public void TryAddItem_ReturnsFailure_WhenPreferredSlotIsOccupied()
        {
            var firstItemResult = _inventory.Add(configId: 1, worldId: Guid.NewGuid(), preferredSlot: 0);
            Assert.IsTrue(firstItemResult.IsSuccess);
            var secondItemResult = _inventory.Add(configId: 2, worldId: Guid.NewGuid(), preferredSlot: 0);
            Assert.IsFalse(secondItemResult.IsSuccess);
            Assert.AreEqual("No free slots available.", secondItemResult.Error);
        }

        [Test]
        public void TryAddItem_ReturnsFailure_WhenItemAlreadyExists()
        {
            var worldId = Guid.NewGuid();
            var firstItemResult = _inventory.Add(configId: 1, worldId: worldId);
            Assert.IsTrue(firstItemResult.IsSuccess);
            var duplicateItemResult = _inventory.Add(configId: 1, worldId: worldId);
            Assert.IsFalse(duplicateItemResult.IsSuccess);
            Assert.AreEqual("Item already exists in inventory.", duplicateItemResult.Error);
        }

        [Test]
        public void TrySelectSlot_ReturnsSuccess_WhenSlotIsValid()
        {
            var result = _inventory.SelectSlot(3);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(3, _inventory.CurrentSlot);
        }

        [Test]
        public void TrySelectSlot_ReturnsFailure_WhenSlotIsInvalid()
        {
            var result = _inventory.SelectSlot(999);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Invalid slot ID.", result.Error);
        }
    }
}