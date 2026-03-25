using Feature.Agent.Application;
using Feature.Agent.Infrastructure;
using Feature.Toolbox.Application;
using Feature.Toolbox.Application.UseCases;
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
    public class SpawnNPCFromToolboxOrchestratorTests
    {
        private Mock<INPCSpawnUseCase> _toolboxSpawnUseCase;
        private Mock<IWorldAgentUseCase> _worldAgentUseCase;
        private Mock<ILogger> _logger;

        private SpawnNPCFromToolboxOrchestrator _sut;

        private List<GameObject> _createdGameObjects;
        private List<Material> _createdMaterials;

        [SetUp]
        public void Setup()
        {
            _toolboxSpawnUseCase = new Mock<INPCSpawnUseCase>();
            _worldAgentUseCase = new Mock<IWorldAgentUseCase>();
            _logger = new Mock<ILogger>();

            _sut = new SpawnNPCFromToolboxOrchestrator(
                _toolboxSpawnUseCase.Object,
                _worldAgentUseCase.Object,
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
            int npcId = 42;
            _toolboxSpawnUseCase
                .Setup(t => t.TrySpawnNPC(npcId))
                .ReturnsAsync(Result<AgentSpawnContext>.Failure("Toolbox error"));

            await _sut.TrySpawn(npcId);

            _logger.Verify(l => l.LogWarning(
                SpawnNPCFromToolboxOrchestrator.LogTag,
                $"Failed to spawn NPC with id {npcId} from toolbox: Toolbox error"),
                Times.Once);

            _worldAgentUseCase.Verify(w => w.SpawnNPC(It.IsAny<IAgentProvider>(), It.IsAny<GameObject>()), Times.Never);
        }

        [Test]
        public async Task TrySpawn_LogsWarning_WhenWorldAgentSpawnFails()
        {
            int npcId = 100;

            var gameObject = CreateGameObject();
            var material = CreateMaterial();
            var config = Mock.Of<IAgentProvider>();

            var spawnContext = new AgentSpawnContext(gameObject, config);

            _toolboxSpawnUseCase
                .Setup(t => t.TrySpawnNPC(npcId))
                .ReturnsAsync(Result<AgentSpawnContext>.Success(spawnContext));

            _worldAgentUseCase
                .Setup(w => w.SpawnNPC(config, gameObject))
                .Returns(Result<AgentContext>.Failure("World spawn error"));

            await _sut.TrySpawn(npcId);

            _logger.Verify(l => l.LogWarning(
                SpawnNPCFromToolboxOrchestrator.LogTag,
                $"Failed to spawn NPC with id {npcId} in the world: World spawn error"),
                Times.Once);
        }

        [Test]
        public async Task TrySpawn_CallsWorldAgent_WhenToolboxSpawnSucceeds()
        {
            int npcId = 55;

            var gameObject = CreateGameObject();
            var material = CreateMaterial();
            var config = Mock.Of<IAgentProvider>();

            var spawnContext = new AgentSpawnContext(gameObject, config);

            _toolboxSpawnUseCase
                .Setup(t => t.TrySpawnNPC(npcId))
                .ReturnsAsync(Result<AgentSpawnContext>.Success(spawnContext));

            _worldAgentUseCase
                .Setup(w => w.SpawnNPC(config, gameObject))
                .Returns(Result<AgentContext>.Success(new AgentContext(gameObject, config.Id, It.IsAny<Guid>())));

            await _sut.TrySpawn(npcId);

            _worldAgentUseCase.Verify(w => w.SpawnNPC(config, gameObject), Times.Once);
            _logger.Verify(l => l.LogWarning(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task TrySpawn_DoesNotThrow_WhenEverythingSucceeds()
        {
            int npcId = 77;

            var gameObject = CreateGameObject();
            var material = CreateMaterial();
            var config = Mock.Of<IAgentProvider>();

            var spawnContext = new AgentSpawnContext(gameObject, config);

            _toolboxSpawnUseCase
                .Setup(t => t.TrySpawnNPC(npcId))
                .ReturnsAsync(Result<AgentSpawnContext>.Success(spawnContext));

            _worldAgentUseCase
                .Setup(w => w.SpawnNPC(config, gameObject))
                .Returns(Result<AgentContext>.Success(new AgentContext(gameObject, config.Id, It.IsAny<Guid>())));

            Assert.DoesNotThrowAsync(async () => await _sut.TrySpawn(npcId));
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