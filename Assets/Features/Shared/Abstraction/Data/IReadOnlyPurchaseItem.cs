using UnityEngine;

public interface IReadOnlyPurchaseItem
{
    bool IsPurchased(PurchaseItemData item);
}
