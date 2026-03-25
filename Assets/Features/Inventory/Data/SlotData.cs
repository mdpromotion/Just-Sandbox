namespace Feature.Inventory.Data
{
    public readonly struct SlotData
    {
        public int Id { get; }
        public string SpriteAddress { get; }
        public SlotData(int id, string spriteAddress)
        {
            Id = id;
            SpriteAddress = spriteAddress;
        }
    }
}