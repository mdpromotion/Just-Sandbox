namespace Shared.Service
{
    public interface IDelay
    {
        void ExecuteAfterDelay(float delay = 1, System.Action action = null);
    }
}