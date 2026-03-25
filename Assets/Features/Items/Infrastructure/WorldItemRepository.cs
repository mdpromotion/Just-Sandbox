#nullable enable
using Feature.Items.Domain;
using Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feature.Items.Infrastructure
{
    public class WorldItemRepository : IWorldItemRepository
    {
        private List<WorldItem> _worldItems;
        public IReadOnlyList<WorldItem> WorldItems => _worldItems.AsReadOnly();

        public WorldItemRepository(List<WorldItem>? worldItems = default)
        {
            _worldItems = worldItems ?? new List<WorldItem>();
        }

        public void Add(WorldItem item)
        {
            if (!_worldItems.Contains(item))
                _worldItems.Add(item);
        }

        public void Delete(WorldItem item)
        {
            if (_worldItems.Contains(item))
                _worldItems.Remove(item);
        }

        public WorldItem? GetById(Guid id)
        {
            return _worldItems.FirstOrDefault(item => item.Id == id);
        }
    }
}