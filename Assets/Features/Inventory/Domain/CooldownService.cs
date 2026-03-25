namespace Feature.Inventory.Domain
{
    public class CooldownService : ICooldownService
    {
        private float _availableAt;

        public bool IsAvaliable(float now)
        {
            return now >= _availableAt;
        }

        public void UpdateLastUseTime(float now, float cooldown)
        {
            _availableAt = now + cooldown;
        }
    }
}