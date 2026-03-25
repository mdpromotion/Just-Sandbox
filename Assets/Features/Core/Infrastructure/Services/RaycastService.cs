using UnityEngine;

namespace Core.Service
{
    public class RaycastService : IRaycastService
    {
        public Vector3 GetRaycastPosition(Vector3 origin, Vector3 direction, float distance)
        {
            Ray ray = new(origin, direction);
            Vector3 basePosition;

            if (Physics.Raycast(ray, out RaycastHit hit, distance))
            {
                basePosition = hit.point;
                Debug.DrawRay(origin, direction * hit.distance, Color.red, 1f);
            }
            else
            {
                basePosition = origin + direction * distance;
                Debug.DrawRay(origin, direction * distance, Color.green, 1f);
            }

            return basePosition;
        }

        public GameObject GetRaycastObject(Vector3 origin, Vector3 direction, float distance, LayerMask layer)
        {
            Ray ray = new(origin, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, distance, layer))
            {
                Debug.DrawRay(origin, direction * hit.distance, Color.red, 1f); 
                return hit.collider.gameObject;
            }

            Debug.DrawRay(origin, direction * distance, Color.green, 1f);
            return null;
        }
    }
}