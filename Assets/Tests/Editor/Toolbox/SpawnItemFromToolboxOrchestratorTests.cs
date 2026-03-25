using Feature.Combat.Application;
using Feature.Inventory.Application;
using Feature.Items.Application;
using Feature.Items.Infrastructure;
using Feature.Toolbox.Application;
using Feature.Toolbox.Data;
using Moq;
using NUnit.Framework;
using Shared.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Tests.Toolbox
{
    [TestFixture]
    public class SpawnItemFromToolboxOrchestratorTests
    {
        private Mock<IItemSpawnUseCase> _toolboxSpawnUseCase;
        private Mock<IWorldItemUseCase> _worldItemUseCase;
        private Mock<IWeaponItemUseCase> _weaponItemUseCase;
        private Mock<IInventoryPickupInput> _inventoryPickupInput;
        private Mock<IReadOnlyToolboxState> _toolboxState;
        private Mock<ILogger> _logger;

        private SpawnItemFromToolboxOrchestrator _sut;

        private List<GameObject> _createdGameObjects;
        private List<Material> _createdMaterials;

        [SetUp]
        public void Setup()
        {
            _toolboxSpawnUseCase = new Mock<IItemSpawnUseCase>();
            _worldItemUseCase = new Mock<IWorldItemUseCase>();
            _weaponItemUseCase = new Mock<IWeaponItemUseCase>();
            _inventoryPickupInput = new Mock<IInventoryPickupInput>();
            _toolboxState = new Mock<IReadOnlyToolboxState>();
            _logger = new Mock<ILogger>();

            _sut = new SpawnItemFromToolboxOrchestrator(
                _toolboxSpawnUseCase.Object,
                _worldItemUseCase.Object,
                _weaponItemUseCase.Object,
                _inventoryPickupInput.Object,
                _toolboxState.Object,
                _logger.Object);

            _createdGameObjects = new List<GameObject>();
            _createdMaterials = new List<Material>();
        }

        private GameObject CreateGameObject()
        {
            var go = new GameObject();
            _createdGameObjects.Add(go);
            return go;
        }

        private Material CreateMaterial()
        {
            var mat = new Material(Shader.Find("Standard"));
            _createdMaterials.Add(mat);
            return mat;
        }

        [Test]
        public async Task TrySpawn_LogsWarning_WhenToolboxSpawnFails()
        {
            int itemId = 10;
            _toolboxState.Setup(t => t.TextureID).Returns(1);

            _toolboxSpawnUseCase
                .Setup(t => t.TrySpawnObject(itemId, 1))
                .ReturnsAsync(Result<ItemSpawnContext>.Failure("Toolbox spawn failed"));

            await _sut.TrySpawn(itemId);

            _logger.Verify(l => l.LogWarning(
                SpawnItemFromToolboxOrchestrator.LogTag,
                "Toolbox spawn failed"),
                Times.Once);

            _worldItemUseCase.Verify(w => w.SpawnItem(It.IsAny<IItemProvider>(), It.IsAny<GameObject>(), It.IsAny<Material>()), Times.Never);
        }

        [Test]
        public async Task TrySpawn_LogsError_WhenWorldItemSpawnFails()
        {
            int itemId = 20;
            _toolboxState.Setup(t => t.TextureID).Returns(2);

            var go = CreateGameObject();
            var mat = CreateMaterial();
            var itemConfig = Mock.Of<IItemProvider>();

            var spawnContext = new ItemSpawnContext(go, mat, itemConfig);

            _toolboxSpawnUseCase
                .Setup(t => t.TrySpawnObject(itemId, 2))
                .ReturnsAsync(Result<ItemSpawnContext>.Success(spawnContext));

            _worldItemUseCase
                .Setup(w => w.SpawnItem(itemConfig, go, mat))
                .Returns(Result<ItemContext>.Failure("World spawn failed"));

            await _sut.TrySpawn(itemId);

            _logger.Verify(l => l.LogError(
                SpawnItemFromToolboxOrchestrator.LogTag,
                "World spawn failed"),
                Times.Once);
        }

        [Test]
        public async Task TrySpawn_CallsSpawnWeapon_WhenItemIsWeapon()
        {
            int itemId = 30;
            _toolboxState.Setup(t => t.TextureID).Returns(3);

            var go = CreateGameObject();
            var mat = CreateMaterial();
            var weaponConfig = Mock.Of<IWeaponProvider>();
            var spawnContext = new ItemSpawnContext(go, mat, weaponConfig);
            var itemContext = new ItemContext(go, weaponConfig.Id, Guid.NewGuid());

            _toolboxSpawnUseCase
                .Setup(t => t.TrySpawnObject(itemId, 3))
                .ReturnsAsync(Result<ItemSpawnContext>.Success(spawnContext));

            _worldItemUseCase
                .Setup(w => w.SpawnItem(weaponConfig, go, mat))
                .Returns(Result<ItemContext>.Success(itemContext));

            await _sut.TrySpawn(itemId);

            _weaponItemUseCase.Verify(w => w.SpawnWeapon(weaponConfig, itemContext.WorldId), Times.Once);
        }

        [Test]
        public async Task TrySpawn_CallsInventoryPickup_WhenSpawnToInventoryIsTrue()
        {
            int itemId = 40;
            _toolboxState.Setup(t => t.TextureID).Returns(4);
            _toolboxState.Setup(t => t.SpawnToInventory).Returns(true);

            var go = CreateGameObject();
            var mat = CreateMaterial();
            var itemConfig = Mock.Of<IItemProvider>();
            var spawnContext = new ItemSpawnContext(go, mat, itemConfig);
            var itemContext = new ItemContext(go, itemConfig.Id, Guid.NewGuid());
            var pickupResult = Result<int>.Success(1);

            _toolboxSpawnUseCase
                .Setup(t => t.TrySpawnObject(itemId, 4))
                .ReturnsAsync(Result<ItemSpawnContext>.Success(spawnContext));

            _worldItemUseCase
                .Setup(w => w.SpawnItem(itemConfig, go, mat))
                .Returns(Result<ItemContext>.Success(itemContext));

            _inventoryPickupInput
                .Setup(i => i.TryPickupSpawnedItem(itemContext))
                .Returns(pickupResult);

            await _sut.TrySpawn(itemId);

            _inventoryPickupInput.Verify(i => i.TryPickupSpawnedItem(itemContext), Times.Once);
            _inventoryPickupInput.Verify(i => i.TrySelectItem(1, It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public async Task TrySpawn_DoesNotThrow_WhenEverythingSucceeds()
        {
            int itemId = 50;
            _toolboxState.Setup(t => t.TextureID).Returns(5);

            var go = CreateGameObject();
            var mat = CreateMaterial();
            var itemConfig = Mock.Of<IItemProvider>();
            var spawnContext = new ItemSpawnContext(go, mat, itemConfig);
            var itemContext = new ItemContext(go, itemConfig.Id, Guid.NewGuid());
            var pickupResult = Result<int>.Success(0);

            _toolboxSpawnUseCase
                .Setup(t => t.TrySpawnObject(itemId, 5))
                .ReturnsAsync(Result<ItemSpawnContext>.Success(spawnContext));

            _worldItemUseCase
                .Setup(w => w.SpawnItem(itemConfig, go, mat))
                .Returns(Result<ItemContext>.Success(itemContext));

            _inventoryPickupInput
                .Setup(i => i.TryPickupSpawnedItem(itemContext))
                .Returns(pickupResult);

            Assert.DoesNotThrowAsync(async () => await _sut.TrySpawn(itemId));
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var go in _createdGameObjects)
            {
                if (go != null)
                    UnityEngine.Object.DestroyImmediate(go);
            }
            _createdGameObjects.Clear();

            foreach (var mat in _createdMaterials)
            {
                if (mat != null)
                    UnityEngine.Object.DestroyImmediate(mat);
            }
            _createdMaterials.Clear();
        }
    }
}