namespace Feature.Inventory.Domain
{
    public interface ICooldownService
    {
        bool IsAvaliable(float now);
        void UpdateLastUseTime(float now, float cooldown);
    }
}