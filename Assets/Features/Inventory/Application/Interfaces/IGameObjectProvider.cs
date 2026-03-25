#nullable enable
using System;
using UnityEngine;

namespace Feature.Inventory.Infrastructure
{
    public interface IGameObjectProvider
    {
        GameObject? Get(Guid itemId);
    }
}
