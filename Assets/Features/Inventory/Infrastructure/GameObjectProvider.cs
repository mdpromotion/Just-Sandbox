#nullable enable
using Shared.Repository;
using Shared.Service;
using System;
using UnityEngine;

namespace Feature.Inventory.Infrastructure
{
    public class GameObjectProvider : IGameObjectProvider
    {
        private readonly IWorldItemLinkService _linkService;
        private readonly IWorldItemRepository _worldItemRepository;

        public GameObjectProvider(IWorldItemLinkService linkService, IWorldItemRepository worldItemRepository)
        {
            _linkService = linkService;
            _worldItemRepository = worldItemRepository;
        }

        public GameObject? Get(Guid itemId)
        {
            var item = _worldItemRepository.GetById(itemId);
            if (item != null && _linkService.TryGetGameObject(item, out var go))
            {
                return go;
            }
            return null;
        }
    }
}