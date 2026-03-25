using Feature.Player.Domain;

namespace Core.Data
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