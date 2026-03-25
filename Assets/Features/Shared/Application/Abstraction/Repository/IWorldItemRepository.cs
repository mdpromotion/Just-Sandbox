#nullable enable
using Feature.Items.Domain;
using System;

namespace Shared.Repository
{
    public interface IWorldItemRepository
    {
        void Add(WorldItem item);
        void Delete(WorldItem item);
        WorldItem? GetById(Guid id);
    }
}