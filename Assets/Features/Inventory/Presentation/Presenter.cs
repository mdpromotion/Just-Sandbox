using Feature.Factory.Infrastructure;
using Feature.Inventory.Application;
using Feature.Inventory.Data;
using Feature.Toolbox.Infrastructure;
using System;
using UnityEngine;
using Zenject;

namespace Feature.Inventory.Presentation
{
    public class Presenter : IInitializable, IDisposable
    {
        private readonly IView _view;
        private readonly IItemConfigService _config;
        private readonly ISpriteFactory _spriteFactory;
        private readonly IInventoryEvents _events;

        private readonly string _nothingSpriteAddress;

        public Presenter(
            IView view,
            IItemConfigService config,
            ISpriteFactory spriteFactory,
            IInventoryEvents events,
            string nothingSpriteAddress = "Assets/Addressables/UI/Nothing.png")
        {
            _view = view;
            _config = config;
            _spriteFactory = spriteFactory;
            _events = events;
            _nothingSpriteAddress = nothingSpriteAddress;
        }

        public void Initialize()
        {
            _events.SlotSelected += OnSlotSelected;
            _events.SlotUnselected += OnSlotUnselected;
        }

        private async void OnSlotSelected(InventoryConfigEventArgs args)
        {
            Sprite sprite = null;

            _view.ToggleIconOutline(args.SlotId, true);

            if (args.ConfigId.HasValue)
            {
                var configResult = _config.GetItemConfig(args.ConfigId.Value);
                if (!configResult.IsSuccess)
                    return;

                var config = configResult.Value;

                if (!string.IsNullOrEmpty(configResult.Value.SpriteAddress))
                {
                    sprite = await _spriteFactory.GetSprite(config.SpriteAddress, config.Name);
                }
            }
            else
            {
                sprite = await _spriteFactory.GetSprite(_nothingSpriteAddress);
            }

            _view.SetIcon(args.SlotId, sprite);
        }
        private async void OnSlotUnselected(InventoryConfigEventArgs args)
        {
            int slotId = args.SlotId;

            _view.ToggleIconOutline(slotId, false);

            if (args.ConfigId.HasValue)
                return;

            Sprite sprite = await _spriteFactory.GetSprite(_nothingSpriteAddress);

            _view.SetIcon(slotId, sprite);
        }

        public void Dispose()
        {
            _events.SlotSelected -= OnSlotSelected;
            _events.SlotUnselected -= OnSlotUnselected;
        }
    }
}