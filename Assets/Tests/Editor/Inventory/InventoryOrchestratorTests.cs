using Feature.Inventory.Application;
using Feature.Inventory.Data;
using Feature.Inventory.Domain;
using Moq;
using NUnit.Framework;
using Shared.Data;
using System;
namespace Tests.Inventory
{

    [TestFixture]
    [Category("Inventory")]
    public class InventoryOrchestratorTests
    {
        private Mock<IManagementUseCase> _management;
        private Mock<IInteractionUseCase> _interaction;
        private Mock<UnityEngine.ILogger> _logger;

        private InventoryOrchestrator _sut;

        [SetUp]
        public void Setup()
        {
            _management = new Mock<IManagementUseCase>();
            _interaction = new Mock<IInteractionUseCase>();
            _logger = new Mock<UnityEngine.ILogger>();

            _sut = new InventoryOrchestrator(
                _management.Object,
                _interaction.Object,
                _logger.Object);
        }

        [Test]
        public void TryPickupSpawnedItem_ReturnsFailure_WhenInventoryIsFailed()
        {
            try
            {
                var itemId = Guid.NewGuid();
                var gameObject = new UnityEngine.GameObject();
                var item = new ItemContext(gameObject, 0, itemId);

                _management.Setup(m => m.TryAddItem(It.IsAny<ItemContext>()))
                    .Returns(Result<InventoryItem>.Success(new InventoryItem(itemId, 0, 0, Guid.NewGuid())));

                _interaction.Setup(i => i.TryPickupItem(It.IsAny<Guid>(), It.IsAny<int>()))
                    .Returns(Result.Failure("Item interaction error"));

                var result = _sut.TryPickupSpawnedItem(item);
                Assert.IsFalse(result.IsSuccess);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
                throw;
            }
        }

        [Test]
        public void TryPickupSpawnedItem_ReturnsFailure_WhenInteractionIsFaled()
        {
            try
            {
                var itemId = Guid.NewGuid();
                var gameObject = new UnityEngine.GameObject();
                var item = new ItemContext(gameObject, 0, itemId);

                _management.Setup(m => m.TryAddItem(It.IsAny<ItemContext>()))
                    .Returns(Result<InventoryItem>.Failure("Inventory full"));

                _interaction.Setup(i => i.TryPickupItem(It.IsAny<Guid>(), It.IsAny<int>()))
                    .Returns(Result.Success());

                var result = _sut.TryPickupSpawnedItem(item);
                Assert.IsFalse(result.IsSuccess);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
                throw;
            }
        }
        [Test]
        public void TrySelectInventoryItem_ReturnsSuccess_OnValidItem()
        {
            try
            {
                _management.Setup(m => m.TrySelectItem(It.IsAny<int>(), It.IsAny<float>()))
                    .Returns(Result<ItemSelectionSnapshot>.Success(new ItemSelectionSnapshot(null, null, 0, 0, 0, 0)));

                _interaction.Setup(i => i.TrySelectItem(It.IsAny<Guid?>(), It.IsAny<Guid?>(), It.IsAny<bool>()))
                    .Returns(Result.Success());

                bool isSuccess = _sut.TrySelectItem(It.IsAny<int>(), It.IsAny<bool>());
                Assert.IsTrue(isSuccess);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
                throw;
            }
        }
        [Test]
        public void TrySelectInventoryItem_ReturnsFailure_WhenInventoryIsFailed()
        {
            try
            {
                _management.Setup(m => m.TrySelectItem(It.IsAny<int>(), It.IsAny<float>()))
                    .Returns(Result<ItemSelectionSnapshot>.Failure("Inventory selected error!"));

                _interaction.Setup(i => i.TrySelectItem(It.IsAny<Guid?>(), It.IsAny<Guid?>(), It.IsAny<bool>()))
                    .Returns(Result.Success());

                bool isSuccess = _sut.TrySelectItem(It.IsAny<int>(), It.IsAny<bool>());
                Assert.IsFalse(isSuccess);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
                throw;
            }
        }
        [Test]
        public void TrySelectInventoryItem_ReturnsFailure_WhenInteractionIsFailed()
        {
            try
            {
                _management.Setup(m => m.TrySelectItem(It.IsAny<int>(), It.IsAny<float>()))
                    .Returns(Result<ItemSelectionSnapshot>.Success(new ItemSelectionSnapshot(null, null, 0, 0, 0, 0)));

                _interaction.Setup(i => i.TrySelectItem(It.IsAny<Guid?>(), It.IsAny<Guid?>(), It.IsAny<bool>()))
                    .Returns(Result.Failure("Item interaction error"));

                bool isSuccess = _sut.TrySelectItem(It.IsAny<int>(), It.IsAny<bool>());
                Assert.IsFalse(isSuccess);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
                throw;
            }
        }

        [Test]
        public void TryDropInventoryItem_ReturnsSuccess_OnValidItem()
        {
            try
            {
                _management.Setup(m => m.TryDeleteItem())
                    .Returns(Result<InventoryItem>.Success(new InventoryItem(Guid.NewGuid(), 0, 0, Guid.NewGuid())));

                _interaction.Setup(i => i.TryDropItem(It.IsAny<Guid>()))
                    .Returns(Result.Success());

                bool isSuccess = _sut.TryDropItem();
                Assert.IsTrue(isSuccess);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
                throw;
            }
        }

        [Test]
        public void TryDropInventoryItem_ReturnsFailure_WhenInventoryIsFailed()
        {
            try
            {
                _management.Setup(m => m.TryDeleteItem())
                    .Returns(Result<InventoryItem>.Failure("Inventory error"));

                _interaction.Setup(i => i.TryDropItem(It.IsAny<Guid>()))
                    .Returns(Result.Success());

                bool isSuccess = _sut.TryDropItem();
                Assert.IsFalse(isSuccess);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
                throw;
            }
        }
        [Test]
        public void TryDropInventoryItem_ReturnsFailure_WhenInteractionIsFailed()
        {
            try
            {
                _management.Setup(m => m.TryDeleteItem())
                    .Returns(Result<InventoryItem>.Success(new InventoryItem(Guid.NewGuid(), 0, 0, Guid.NewGuid())));


                _interaction.Setup(i => i.TryDropItem(It.IsAny<Guid>()))
                    .Returns(Result.Failure("Interaction error"));

                bool isSuccess = _sut.TryDropItem();
                Assert.IsFalse(isSuccess);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
                throw;
            }
        }
    }
}