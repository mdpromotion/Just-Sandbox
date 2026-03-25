using Core.Service;
using Feature.Core.Infrastructure;
using System;
using UnityEngine;
using Zenject;

namespace Feature.Player.Infrastructure
{
    [RequireComponent(typeof(EntityWorldBind))]
    public class PlayerSpawner : MonoBehaviour
    {
        private Domain.Player _player;
        private IWorldEntityService _worldEntityService;
        private EntityWorldBind _entityWorldBind;

        private void Awake()
        {
            _entityWorldBind = GetComponent<EntityWorldBind>();
        }

        [Inject]
        public void Construct(IWorldEntityService worldEntityService, Domain.Player player)
        {
            _worldEntityService = worldEntityService;
            _player = player;
        }

        public void Start()
        {
            _entityWorldBind.Bind(_player, _player);
            _worldEntityService.Bind(_player, gameObject);
        }

    }
}