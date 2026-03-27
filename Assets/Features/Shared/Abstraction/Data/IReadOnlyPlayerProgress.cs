namespace Feature.Storage.Domain
{
    public interface IReadOnlyPlayerProgress
    {
        bool IsTutorialCompleted { get; }
    }
}