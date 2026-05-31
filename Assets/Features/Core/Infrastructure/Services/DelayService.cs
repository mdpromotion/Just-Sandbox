using System;
using System.Collections;
using Shared.Service;
using UnityEngine;

namespace Features.Core.Infrastructure.Services
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