using Feature.Player.Domain;
using Feature.Toolbox.Application;
using Feature.Toolbox.Domain;
using Moq;
using NUnit.Framework;
using System;
using UnityEngine;

namespace Tests.Toolbox
{
    [TestFixture]
    public class MenuUseCaseTests
    {
        private Mock<IReadOnlyPlayer> _playerState;
        private Mock<IGameState> _gameState;
        private Mock<IToolboxState> _toolboxState;
        private Mock<ILogger> _logger;

        private MenuUseCase _sut;

        private bool _eventFired;
        private bool _eventValue;

        [SetUp]
        public void Setup()
        {
            _playerState = new Mock<IReadOnlyPlayer>();
            _gameState = new Mock<IGameState>();
            _toolboxState = new Mock<IToolboxState>();
            _logger = new Mock<ILogger>();

            _sut = new MenuUseCase(
                _playerState.Object,
                _gameState.Object,
                _toolboxState.Object,
                _logger.Object);

            _eventFired = false;
            _eventValue = false;

            _sut.ToolboxToggled += value =>
            {
                _eventFired = true;
                _eventValue = value;
            };
        }

        [Test]
        public void ToggleToolbox_ReturnsFalse_WhenPlayerDead()
        {
            _playerState.Setup(p => p.IsAlive).Returns(false);

            bool result = _sut.ToggleToolbox();

            Assert.IsFalse(result);
            _logger.Verify(l => l.LogWarning(It.IsAny<string>(), "Player is dead. Toolbox cannot be toggled."), Times.Once);
            _gameState.Verify(g => g.ToggleMenu(It.IsAny<bool>()), Times.Never);
            Assert.IsFalse(_eventFired);
        }

        [Test]
        public void ToggleToolbox_TogglesState_WhenPlayerAlive()
        {
            _playerState.Setup(p => p.IsAlive).Returns(true);
            _toolboxState.Setup(t => t.ToggleToolbox()).Returns(true);

            bool result = _sut.ToggleToolbox();

            Assert.IsTrue(result);
            _toolboxState.Verify(t => t.ToggleToolbox(), Times.Once);
            _gameState.Verify(g => g.ToggleMenu(true), Times.Once);
            Assert.IsTrue(_eventFired);
            Assert.IsTrue(_eventValue);
        }

        [Test]
        public void SelectTexture_CallsSetTextureID()
        {
            int textureId = 42;

            _sut.SelectTexture(textureId);

            _toolboxState.Verify(t => t.SetTextureID(textureId), Times.Once);
        }

        [Test]
        public void ToggleInventorySpawn_SwitchesState()
        {
            _toolboxState.Setup(t => t.SpawnToInventory).Returns(false);

            bool result = _sut.ToggleInventorySpawn();

            Assert.IsTrue(result);
            _toolboxState.Verify(t => t.SetSpawnToInventory(true), Times.Once);
        }
    }
}