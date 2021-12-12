using System;
using System.Threading;
using System.Threading.Tasks;
using Code.Abstractions;
using Code.Abstractions.Command;
using Code.Tools;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Core.Unit
{
    public class WarriorPatrol:CommandExecutorBase<IPatrolCommand>, IChangeAnimation, IAwaitable<AsyncExtensions.Void>
    {
        public class StopPatrol:AwaiterBase<AsyncExtensions.Void>
        {
            private readonly WarriorPatrol _movementStop;

            public StopPatrol(WarriorPatrol movementStop)
            {
                _movementStop = movementStop;
                _movementStop.Stop += onStop;
            }

            private void onStop()
            {
                _movementStop.Stop -= onStop;
                ONWaitFinish(new AsyncExtensions.Void());
            }
        }
        
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private WarriorStop _warriorStop;
        [SerializeField] private WarriorAnimation _warriorAnimation;
        private Vector3[] _points = new Vector3[2];
        public event Action Stop;
        private int _current = 0;
        private bool _isPatrul;

        public Action<AnimationStates> ChangeAnimation { get; set; }

        [Inject]
        private void Init()
        {
            _warriorAnimation.Init(this);
        }

        public override async Task ExecuteSpecificCommand(IPatrolCommand command)
        {
            _points[0] = command.Current;
            _points[1] = command.Target;
            ChangeAnimation.Invoke(AnimationStates.Run);
            _warriorStop.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                _isPatrul = true;
                await this.WithCancellation(_warriorStop.CancellationTokenSource.Token);
            }
            catch (Exception e)
            {
                _isPatrul = false;
                _navMeshAgent.isStopped = true;
                _navMeshAgent.ResetPath();
                ChangeAnimation?.Invoke(AnimationStates.Idle);
            }
            Debug.Log("end");
            _warriorStop.CancellationTokenSource = null;
        }

        private void Update()
        {
            if (!_isPatrul)
                return;
            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0)
                    {
                        _navMeshAgent.destination = _points[_current%_points.Length];
                        _current++;
                    }
                }
            }
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopPatrol(this);
    }
}