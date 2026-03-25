using UnityEngine;

public interface IInventoryService
{
    bool IsAvailable();
    void UpdateLastUseTime();
    int? GetFreeSlot();
}
