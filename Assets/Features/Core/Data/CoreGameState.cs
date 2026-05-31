using Core.Data;
using Feature.Player.Domain;
using Features.Core.Interfaces;

namespace Features.Core.Data
{
    public class CoreGameStates : IReadOnlyCoreGameStates
    {
        public IReadOnlyGameState Game { get; }
        public IReadOnlyPlayer Player { get; private set; }
        public bool IsPlayerControllable => !Game.IsPaused && Player.IsAlive;

        public CoreGameStates(
            IReadOnlyGameState game,
            IReadOnlyPlayer player)
        {
            Game = game;
            Player = player;
        }
    }
}