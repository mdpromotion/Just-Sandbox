#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feature.Combat.Domain
{
    public class WeaponInventory : IReadOnlyWeaponInventory
    {
        private List<IWeapon> _inventoryWeapons;
        public IReadOnlyList<IWeapon> Weapons => _inventoryWeapons.AsReadOnly();

        public WeaponInventory()
        {
            _inventoryWeapons = new List<IWeapon>();
        }

        public Result<IWeapon> Add(IWeapon weapon)
        {
            if (_inventoryWeapons.Contains(weapon))
                return Result<IWeapon>.Failure("Item already exists in inventory.");

            _inventoryWeapons.Add(weapon);
            return Result<IWeapon>.Success(weapon);
        }

        public void Delete(IWeapon weapon)
        {
            _inventoryWeapons.Remove(weapon);
        }

        public IWeapon? GetById(Guid id)
        {
            return _inventoryWeapons.FirstOrDefault(item => item.Id == id);
        }
        public IWeapon? GetByWorldId(Guid worldId)
        {
            return _inventoryWeapons.FirstOrDefault(item => item.WorldId == worldId);
        }
    }
}