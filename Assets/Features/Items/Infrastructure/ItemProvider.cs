using UnityEngine;

namespace Feature.Items.Infrastructure
{
    public class ItemProvider : IItemProvider
    {
        private readonly ItemData _itemData;

        public int Id => _itemData.Id;
        public string Name => _itemData.Name;
        public ItemType Type => _itemData.Type;
        public string PrefabAddress => _itemData.PrefabAddress;
        public string SpriteAddress => _itemData.SpriteAddress;
        public Vector3 DisplayOffset => _itemData.DisplayOffset;

        public ItemProvider(ItemData itemData)
        {
            _itemData = itemData;
        }
    }
}