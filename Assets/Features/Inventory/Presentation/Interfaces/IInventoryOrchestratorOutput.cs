using UnityEngine;

namespace Feature.Inventory.Application
{
    public interface IInventoryOrchestratorOutput
    {
        void SetItemSelected(int index, bool selected, int? configId = null);
    }
}