using Feature.Agent.Infrastructure;
using Feature.Factory.Infrastructure;
using Feature.Items.Data;
using Feature.Toolbox.Application;
using Moq;
using NUnit.Framework;
using Shared.Service;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Threading.Tasks;

namespace Tests.Toolbox
{
    [TestFixture]
    public class NPCSpawnUseCaseTests
    {
        private Mock<IGameObjectFactory> _factory;
        private Mock<ITransformWorldService> _worldService;
        private Mock<IAgentConfigService> _configService;

        private NPCSpawnUseCase _sut;

        [SetUp]
        public void Setup()
        {
            _factory = new Mock<IGameObjectFactory>();
            _worldService = new Mock<ITransformWorldService>();
            _configService = new Mock<IAgentConfigService>();

            _sut = new NPCSpawnUseCase(
                _factory.Object,
                _worldService.Object,
                _configService.Object);
        }

        private Mock<IAgentProvider> CreateAgentProviderMock(int configId, string prefabAddress = "npcPrefab")
        {
            var mock = new Mock<IAgentProvider>();
            mock.SetupGet(a => a.PrefabAddress).Returns(prefabAddress);
            mock.SetupGet(a => a.Id).Returns(configId);
            return mock;
        }

        [Test]
        public async Task TrySpawnNPC_ReturnsFailure_WhenAgentConfigFails()
        {
            int npcId = 1;
            _configService
                .Setup(c => c.GetAgentConfig(npcId))
                .Returns(Result<IAgentProvider>.Failure("Config error"));

            var result = await _sut.TrySpawnNPC(npcId).AsTask();

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Config error", result.Error);
        }

        [Test]
        public async Task TrySpawnNPC_ReturnsFailure_WhenSpawnPointFails()
        {
            int npcId = 2;
            var agentConfig = CreateAgentProviderMock(npcId).Object;

            _configService
                .Setup(c => c.GetAgentConfig(npcId))
                .Returns(Result<IAgentProvider>.Success(agentConfig));

            _worldService
                .Setup(w => w.GetSpawnPoint())
                .Returns(Result<TransformProvider>.Failure("No spawn point"));

            var result = await _sut.TrySpawnNPC(npcId).AsTask();

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No spawn point", result.Error);
        }

        [Test]
        public async Task TrySpawnNPC_ReturnsFailure_WhenFactoryReturnsNull()
        {
            int npcId = 3;
            var agentConfig = CreateAgentProviderMock(npcId).Object;

            _configService
                .Setup(c => c.GetAgentConfig(npcId))
                .Returns(Result<IAgentProvider>.Success(agentConfig));

            var spawnPoint = new TransformProvider(Vector3.zero, Quaternion.identity);
            _worldService
                .Setup(w => w.GetSpawnPoint())
                .Returns(Result<TransformProvider>.Success(spawnPoint));

            _factory
                .Setup(f => f.SpawnObject(agentConfig.PrefabAddress, spawnPoint.Position, spawnPoint.Rotation, null))
                .Returns(UniTask.FromResult<GameObject>(null));

            var result = await _sut.TrySpawnNPC(npcId).AsTask();

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual($"Failed to spawn npc for item id {npcId}", result.Error);
        }

        [Test]
        public async Task TrySpawnNPC_ReturnsSuccess_WhenAllSucceeds()
        {
            int npcId = 4;
            var agentConfig = CreateAgentProviderMock(npcId).Object;

            _configService
                .Setup(c => c.GetAgentConfig(npcId))
                .Returns(Result<IAgentProvider>.Success(agentConfig));

            var spawnPoint = new TransformProvider(Vector3.zero, Quaternion.identity);
            _worldService
                .Setup(w => w.GetSpawnPoint())
                .Returns(Result<TransformProvider>.Success(spawnPoint));

            var go = new GameObject();
            _factory
                .Setup(f => f.SpawnObject(agentConfig.PrefabAddress, spawnPoint.Position, spawnPoint.Rotation, null))
                .Returns(UniTask.FromResult(go));

            var result = await _sut.TrySpawnNPC(npcId).AsTask();

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(go, result.Value.Object);
            Assert.AreEqual(agentConfig, result.Value.Config);

            Object.DestroyImmediate(go);
        }
    }
}