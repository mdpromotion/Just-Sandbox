/*using Game;
using System.Collections;
using UnityEngine;

public class GrenadeModule : MonoBehaviour
{
    [SerializeField] private GameObject grenadePin;
    [SerializeField] private GameObject grenadeObject;
    [SerializeField] private ExplosionEffect explosionEffect;
    [SerializeField] private LayerMask explosionMask;

    private Rigidbody grenadeRigidbody;

    private Vector3 initialPositionForPin;
    private Quaternion initialRotationForPin;

    private void Start()
    {
        grenadeRigidbody = GetComponent<Rigidbody>();

        initialPositionForPin = grenadePin.transform.localPosition;
        initialRotationForPin = grenadePin.transform.localRotation;
    }

    public void ThrowGrenade(Vector3 originPosition, Vector3 forward)
    {
        if (!grenadeRigidbody)
            grenadeRigidbody = GetComponent<Rigidbody>();

        grenadePin.transform.localPosition = initialPositionForPin;
        grenadePin.transform.localRotation = initialRotationForPin;

        transform.position = originPosition;
        grenadeObject.SetActive(true);

        grenadeRigidbody.linearVelocity = Vector3.zero;
        grenadeRigidbody.angularVelocity = Vector3.zero;

        grenadeRigidbody.AddForce(forward.normalized * 10f, ForceMode.Impulse);
        StartCoroutine(WaitUntilExplode());
    }
    public void ExplodeGrenade()
    {
        explosionEffect.transform.position = grenadeObject.transform.position;
        explosionEffect.Explode();
        grenadeObject.SetActive(false);
        weaponHandler.ExplodeHandler(grenadeObject.transform);
        StartCoroutine(WaitUntillDisable());
    }
    IEnumerator WaitUntilExplode()
    {
        yield return new WaitForSeconds(3f);
        ExplodeGrenade();
    }
    IEnumerator WaitUntillDisable()
    {
        yield return new WaitForSeconds(5f);
        ObjectPool.Instance.Release(gameObject);
    }
}
*/