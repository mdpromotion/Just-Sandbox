using Feature.Combat.Application;
using Feature.Combat.Domain;
using Feature.Inventory.Application;
using Feature.Inventory.Data;
using Feature.Inventory.Domain;
using System;
using Zenject;

namespace Feature.Combat.Presentation
{
    public class Presenter : IInitializable, IDisposable
    {
        private readonly IUseEvents _events;
        private readonly IInventoryEvents _inventoryEvents;
        private readonly IView _view;
        private readonly IReadOnlyWeaponInventory _inventory;

        public Presenter(
            IUseEvents events, 
            IInventoryEvents inventoryEvents,
            IView view,
            IReadOnlyWeaponInventory inventory)
        {
            _events = events;
            _inventoryEvents = inventoryEvents;
            _view = view;
            _inventory = inventory;
        }

        public void Initialize()
        {
            _events.Used += OnUsed;
            _events.Reloaded += OnReloaded;
            _inventoryEvents.SlotSelected += OnSlotSelected;
        }

        private void OnUsed(IWeapon weapon)
        {
            ChangeAmmo(weapon.CurrentAmmo, weapon.ReserveAmmo);
        }

        private void OnReloaded(IWeapon weapon)
        {
            ChangeAmmo(weapon.CurrentAmmo, weapon.ReserveAmmo);
        }

        private void OnSlotSelected(InventoryConfigEventArgs item)
        {
            if (item.WorldId.HasValue)
            {
                var weapon = _inventory.GetByWorldId(item.WorldId.Value);
                if (weapon == null)
                {
                    _view.ToggleAmmoText(false);
                    return;
                }

                _view.ToggleAmmoText(true);
                ChangeAmmo(weapon.CurrentAmmo, weapon.ReserveAmmo);
            }
            else
            {
                _view.ToggleAmmoText(false);
            }
        }

        private void ChangeAmmo(int currentAmmo, int reserveAmmo)
        {
            _view.SetAmmoText($"{currentAmmo} | {reserveAmmo}");
        }

        public void Dispose()
        {
            _events.Used -= OnUsed;
            _events.Reloaded -= OnReloaded;
            _inventoryEvents.SlotSelected -= OnSlotSelected;
        }
    }
}