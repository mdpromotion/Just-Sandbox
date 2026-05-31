using System;

namespace Features.Combat.Application.Interfaces
{
    public interface IReadOnlyPlayerInventory
    {
        Guid GetSelectedWorldId();
    }
}