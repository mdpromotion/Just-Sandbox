using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    Popup,
    Coin,
    Grenade
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    private Dictionary<PoolType, List<GameObject>> pools = new Dictionary<PoolType, List<GameObject>>();
    public int batchSize = 50;

    void Awake()
    {
        Instance = this;

        foreach (PoolType type in System.Enum.GetValues(typeof(PoolType)))
            pools[type] = new List<GameObject>();

        StartCoroutine(InitializePool());
    }

    private IEnumerator InitializePool()
    {
        int count = 0;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

            if (child.CompareTag("Popup"))
                pools[PoolType.Popup].Add(child.gameObject);
            else if (child.CompareTag("Coin"))
                pools[PoolType.Coin].Add(child.gameObject);
            else if (child.CompareTag("Grenade"))
                pools[PoolType.Grenade].Add(child.gameObject);

            count++;
            if (count % batchSize == 0)
                yield return null;
        }
    }

    private int _lastTarget = -1;

    void Update()
    {
        if (Application.targetFrameRate != _lastTarget)
        {
            _lastTarget = Application.targetFrameRate;
            Debug.Log($"TargetFrameRate changed to {_lastTarget}\n{Environment.StackTrace}");
        }
    }

    public GameObject Get(PoolType type)
    {
        var pool = pools[type];

        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        return null;
    }

    public void Release(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
    }
}
