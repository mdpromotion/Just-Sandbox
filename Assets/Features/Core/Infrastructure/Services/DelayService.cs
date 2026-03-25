using UnityEngine;
using System;
using System.Collections;
using Shared.Service;

namespace Core.Service
{
    public class DelayService : MonoBehaviour, IDelay
    {
        public void ExecuteAfterDelay(float delay = 1, Action action = null)
        {
            StartCoroutine(DelayExecute(delay, action));
        }

        private IEnumerator DelayExecute(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}