using System;
using Code.Abstractions;
using Code.Abstractions.Command;
using Code.Tools;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Core.Unit
{
    public class WarriorMovementStop:MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        public class StopAwaiter:AwaiterBase<AsyncExtensions.Void>
        {
            private readonly WarriorMovementStop _movementStop;

            public StopAwaiter(WarriorMovementStop movementStop)
            {
                _movementStop = movementStop;
                _movementStop.Stop += onStop;
            }

            private void onStop()
            {
                _movementStop.Stop -= onStop;
                _isCompleted = true;
                StartEvent();
            }

            public override AsyncExtensions.Void GetResult() => 
                new AsyncExtensions.Void();
        }

        [SerializeField] private NavMeshAgent _navMeshAgent;
        public event Action Stop;

        private void Update()
        {
            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0)
                    {
                        Stop?.Invoke();
                    }
                }
            }
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => 
            new StopAwaiter(this);
    }
}