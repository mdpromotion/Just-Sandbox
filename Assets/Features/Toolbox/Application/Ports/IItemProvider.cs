using UnityEngine;

namespace Feature.Items.Infrastructure
{
    public interface IItemProvider
    {
        int Id { get; }
        string Name { get; }
        ItemType Type { get; }
        string PrefabAddress { get; }
        string SpriteAddress { get; }
        Vector3 DisplayOffset { get; }
    }
}