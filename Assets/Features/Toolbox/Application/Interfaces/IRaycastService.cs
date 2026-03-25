using UnityEngine;

namespace Core.Service
{
    public interface IRaycastService
    {
        Vector3 GetRaycastPosition(Vector3 origin, Vector3 direction, float distance);
        GameObject GetRaycastObject(Vector3 origin, Vector3 direction, float distance, LayerMask layer);
    }
}