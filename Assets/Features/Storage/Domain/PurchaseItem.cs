using System.Collections.Generic;
using UnityEngine;

public enum PurchaseItemData { None, CheatMenu, Rifle, M4, Set, Turret }
public class PurchaseItem : IReadOnlyPurchaseItem
{
    private readonly Dictionary<PurchaseItemData, bool> _purchases = new();

    public bool IsPurchased(PurchaseItemData item)
    {
        if (item == PurchaseItemData.None) return false;
        return _purchases.TryGetValue(item, out var purchased) && purchased;
    }
    public void SetPurchase(PurchaseItemData item, bool purchased)
    {
        if (item == PurchaseItemData.None) return;
        _purchases[item] = purchased;
    }
}
