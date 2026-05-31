using System;
using Features.Agent.Application.Controllers.Interfaces;
using Features.Core.Infrastructure;
using Shared.Domain;
using UnityEngine;

namespace Features.Agent.Infrastructure.Controllers
{
    public class TriggerHandler : MonoBehaviour, ITriggerHandler
    {
        private Collider _targetCollider;
        private ITarget _currentTarget;

        public event Action<ITarget> TargetEntered;
        public event Action<ITarget> TargetExited;


        private void OnTriggerEnter(Collider other)
        {
            if (_currentTarget != null) return;

            if (other.CompareTag("Player"))
            {
                if (other.gameObject.TryGetComponent<EntityWorldBind>(out var target))
                {
                    _currentTarget = target.AsTarget;
                    _targetCollider = other;
                    TargetEntered?.Invoke(_currentTarget);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other == _targetCollider)
                {
                    TargetExited?.Invoke(_currentTarget);
                    _currentTarget = null;
                }
            }
        }
    }
}
