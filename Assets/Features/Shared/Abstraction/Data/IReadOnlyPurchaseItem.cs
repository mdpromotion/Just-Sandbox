namespace Feature.Storage.Domain
{
    public interface IReadOnlyPurchaseItem
    {
        bool IsPurchased(PurchaseItemData item);
    }
}