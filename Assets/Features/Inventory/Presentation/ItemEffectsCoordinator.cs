using Feature.Inventory.Application;
using Feature.Inventory.Data;
using Shared.Service;
using System;
using UnityEngine;
using Zenject;

namespace Feature.Inventory.Presentation
{
    public class ItemEffectsCoordinator : IInitializable, IDisposable
    {
        private readonly IItemAnimator _animator;
        private readonly IInventoryEvents _events;

        public ItemEffectsCoordinator(
            IItemAnimator animator,
            IInventoryEvents events)
        {
            _animator = animator;
            _events = events;
        }

        public void Initialize()
        {
            _events.SlotUnselected += OnSlotUnselected;
            _events.SlotSelected += OnSlotSelected;
        }

        private void OnSlotUnselected(InventoryConfigEventArgs args)
        {
            _animator.ForceStopAnimation();
        }
        private void OnSlotSelected(InventoryConfigEventArgs args)
        {
            if (args.ConfigId.HasValue)
                _animator.RebindAnimator();

            _animator.PlayEquipAnimation();
        }

        public void Dispose()
        {
            _events.SlotUnselected -= OnSlotUnselected;
            _events.SlotSelected -= OnSlotSelected;
        }
    }
}