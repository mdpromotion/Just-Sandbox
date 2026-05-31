using Feature.Items.Infrastructure;
using Features.Combat.Data;

public interface IWeaponProvider : IItemProvider
{
    int Damage { get; }
    float Range { get; }
    float Cooldown { get; }
    float Knockback { get; }
    int MaxAmmoInClip { get; }
    int ReserveAmmo { get; }
    WeaponType WeaponType { get; }
}
