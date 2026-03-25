using System;

namespace Feature.Items.Domain
{
    /// <summary>
    /// Represents an immutable item within the world, identified by a unique identifier, a name, and a configuration
    /// identifier.
    /// </summary>
    /// <remarks>Instances of the WorldItem class are immutable after creation. Each item is uniquely
    /// identified by its Id property, and its Name and ConfigId provide descriptive and configuration information,
    /// respectively. This class is typically used to encapsulate item data within a larger world or domain
    /// context.</remarks>
    public class WorldItem
    {
        public Guid Id { get; }
        public string Name { get; }
        public int ConfigId { get; }

        public WorldItem(Guid id, string name, int configId)
        {
            Id = id;
            Name = name;
            ConfigId = configId;
        }
    }
}