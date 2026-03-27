namespace Feature.Storage.Domain
{
    public class PlayerProgress : IReadOnlyPlayerProgress
    {
        public bool IsTutorialCompleted { get; private set; }
        public PlayerProgress(bool isTutorialCompleted = false)
        {
            IsTutorialCompleted = isTutorialCompleted;
        }
        public void CompleteTutorial()
        {
            IsTutorialCompleted = true;
        }
    }
}