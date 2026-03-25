using Feature.Factory.Infrastructure;
using Feature.Items.Data;
using Feature.Items.Infrastructure;
using Feature.Toolbox.Application;
using Feature.Toolbox.Infrastructure;
using Moq;
using NUnit.Framework;
using Shared.Service;
using System.Threading.Tasks;
using UnityEngine;

namespace Tests.Toolbox
{
    [TestFixture]
    public class ItemSpawnUseCaseTests
    {
        private Mock<IGameObjectFactory> _factory;
        private Mock<IMaterialFactory> _materialFactory;
        private Mock<ITransformWorldService> _worldService;
        private Mock<IItemConfigService> _itemConfig;
        private Mock<ITextureConfigService> _textureConfig;

        private ItemSpawnUseCase _sut;

        private GameObject _createdGameObject;
        private Material _createdMaterial;

        [SetUp]
        public void Setup()
        {
            _factory = new Mock<IGameObjectFactory>();
            _materialFactory = new Mock<IMaterialFactory>();
            _worldService = new Mock<ITransformWorldService>();
            _itemConfig = new Mock<IItemConfigService>();
            _textureConfig = new Mock<ITextureConfigService>();

            _sut = new ItemSpawnUseCase(
                _factory.Object,
                _materialFactory.Object,
                _worldService.Object,
                _itemConfig.Object,
                _textureConfig.Object);
        }

        private GameObject CreateGameObject()
        {
            _createdGameObject = new GameObject();
            return _createdGameObject;
        }

        private Material CreateMaterial()
        {
            _createdMaterial = new Material(Shader.Find("Standard"));
            return _createdMaterial;
        }

        [Test]
        public async Task TrySpawnObject_ReturnsFailure_WhenItemConfigFails()
        {
            int itemId = 1;
            int textureId = 10;

            _itemConfig
                .Setup(i => i.GetItemConfig(itemId))
                .Returns(Result<IItemProvider>.Failure("Config error"));

            var result = await _sut.TrySpawnObject(itemId, textureId);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Config error", result.Error);
        }

        [Test]
        public async Task TrySpawnObject_ReturnsFailure_WhenSpawnPointFails()
        {
            int itemId = 2;
            int textureId = 20;
            var itemConfig = Mock.Of<IItemProvider>();

            _itemConfig
                .Setup(i => i.GetItemConfig(itemId))
                .Returns(Result<IItemProvider>.Success(itemConfig));

            _worldService
                .Setup(w => w.GetSpawnPoint())
                .Returns(Result<TransformProvider>.Failure("No spawn point"));

            var result = await _sut.TrySpawnObject(itemId, textureId);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No spawn point", result.Error);
        }

        [Test]
        public async Task TrySpawnObject_ReturnsFailure_WhenMaterialAddressFails()
        {
            int itemId = 3;
            int textureId = 30;
            var itemConfig = Mock.Of<IItemProvider>();
            var spawnPoint = new TransformProvider(Vector3.zero, Quaternion.identity);

            _itemConfig
                .Setup(i => i.GetItemConfig(itemId))
                .Returns(Result<IItemProvider>.Success(itemConfig));

            _worldService
                .Setup(w => w.GetSpawnPoint())
                .Returns(Result<TransformProvider>.Success(spawnPoint));

            _textureConfig
                .Setup(t => t.GetMaterialAddress(textureId))
                .Returns(Result<string>.Failure("Material address error"));

            var result = await _sut.TrySpawnObject(itemId, textureId);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Material address error", result.Error);
        }

        [Test]
        public async Task TrySpawnObject_ReturnsFailure_WhenMaterialFactoryReturnsNull()
        {
            int itemId = 4;
            int textureId = 40;
            var itemConfig = Mock.Of<IItemProvider>();
            var spawnPoint = new TransformProvider(Vector3.zero, Quaternion.identity);

            _itemConfig
                .Setup(i => i.GetItemConfig(itemId))
                .Returns(Result<IItemProvider>.Success(itemConfig));

            _worldService
                .Setup(w => w.GetSpawnPoint())
                .Returns(Result<TransformProvider>.Success(spawnPoint));

            _textureConfig
                .Setup(t => t.GetMaterialAddress(textureId))
                .Returns(Result<string>.Success("matAddress"));

            _materialFactory
                .Setup(m => m.GetMaterial("matAddress"))
                .ReturnsAsync((Material)null);

            var result = await _sut.TrySpawnObject(itemId, textureId);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Failed to load material for texture id {textureId}", result.Error);
        }

        [Test]
        public async Task TrySpawnObject_ReturnsFailure_WhenFactoryReturnsNull()
        {
            int itemId = 5;
            int textureId = 50;
            var itemConfig = Mock.Of<IItemProvider>();
            var spawnPoint = new TransformProvider(Vector3.zero, Quaternion.identity);
            var material = CreateMaterial();

            _itemConfig
                .Setup(i => i.GetItemConfig(itemId))
                .Returns(Result<IItemProvider>.Success(itemConfig));

            _worldService
                .Setup(w => w.GetSpawnPoint())
                .Returns(Result<TransformProvider>.Success(spawnPoint));

            _textureConfig
                .Setup(t => t.GetMaterialAddress(textureId))
                .Returns(Result<string>.Success("matAddress"));

            _materialFactory
                .Setup(m => m.GetMaterial("matAddress"))
                .ReturnsAsync(material);

            _factory
                .Setup(f => f.SpawnObject(itemConfig.PrefabAddress, spawnPoint.Position, spawnPoint.Rotation, material))
                .ReturnsAsync((GameObject)null);

            var result = await _sut.TrySpawnObject(itemId, textureId);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Failed to spawn object for item id {itemId}", result.Error);
        }

        [Test]
        public async Task TrySpawnObject_ReturnsSuccess_WhenAllSucceeds()
        {
            int itemId = 6;
            int textureId = 60;
            var itemConfig = Mock.Of<IItemProvider>();
            var spawnPoint = new TransformProvider(Vector3.zero, Quaternion.identity);
            var material = CreateMaterial();
            var go = CreateGameObject();

            _itemConfig
                .Setup(i => i.GetItemConfig(itemId))
                .Returns(Result<IItemProvider>.Success(itemConfig));

            _worldService
                .Setup(w => w.GetSpawnPoint())
                .Returns(Result<TransformProvider>.Success(spawnPoint));

            _textureConfig
                .Setup(t => t.GetMaterialAddress(textureId))
                .Returns(Result<string>.Success("matAddress"));

            _materialFactory
                .Setup(m => m.GetMaterial("matAddress"))
                .ReturnsAsync(material);

            _factory
                .Setup(f => f.SpawnObject(itemConfig.PrefabAddress, spawnPoint.Position, spawnPoint.Rotation, material))
                .ReturnsAsync(go);

            var result = await _sut.TrySpawnObject(itemId, textureId);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(go, result.Value.Object);
            Assert.AreEqual(material, result.Value.Material);
            Assert.AreEqual(itemConfig, result.Value.Config);
        }

        [TearDown]
        public void TearDown()
        {
            if (_createdGameObject != null)
                Object.DestroyImmediate(_createdGameObject);

            if (_createdMaterial != null)
                Object.DestroyImmediate(_createdMaterial);
        }
    }
}