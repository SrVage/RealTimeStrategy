using System;
using System.Threading;
using System.Threading.Tasks;
using Code.Abstractions;
using Code.Abstractions.Command;
using Code.Core.CommandExecutors;
using Code.Tools;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Core.Unit
{
    public class WarriorAttack:CommandExecutorBase<IAttackCommand>, IDamageDealer
    {
        private int AnimationRun = Animator.StringToHash("Run");
        private int AnimationAttack = Animator.StringToHash("Attack");
        private int AnimationIdle = Animator.StringToHash("Idle");
        public class AttackOperation:IAwaitable<AsyncExtensions.Void>
        {
            private const float AtackingDistance = 0.9f;
            private const int SleepThreadTime = 400;

            public class AttackOperationAwaiter : AwaiterBase<AsyncExtensions.Void>
            {
                private AttackOperation _attackOperation;
                public AttackOperationAwaiter(AttackOperation attackOperation)
                {
                    _attackOperation = attackOperation;
                    _attackOperation.OnCompleted += OnCompleted;
                }

                private void OnCompleted()
                {
                    _attackOperation.OnCompleted -= OnCompleted;
                    ONWaitFinish(new AsyncExtensions.Void());
                }
            }

            private event Action OnCompleted;
            private readonly WarriorAttack _warriorAttack;
            private readonly ICanAttacked _target;
            private bool _isCancelled;

            public AttackOperation(WarriorAttack warriorAttack, ICanAttacked target)
            {
                _warriorAttack = warriorAttack;
                _target = target;
                var thread = new Thread(AttackAlgorithm);
                thread.Start();
            }

            public void Cancel()
            {
                _isCancelled = true;
                OnCompleted?.Invoke();
            }

            private void AttackAlgorithm(object obj)
            {
                while (true)
                {
                    if (
                        _warriorAttack == null
                        || _warriorAttack._selfHealth.Health == 0
                        || _target.Health == 0
                        || _isCancelled
                    )
                    {
                        OnCompleted?.Invoke();
                        return;
                    }
                    var targetPosition = default(Vector3);
                    var selfPosition = default(Vector3);
                    var selfRotation = default(Quaternion);
                    lock (_warriorAttack)
                    {
                        targetPosition = _warriorAttack._targetPosition;
                        selfPosition = _warriorAttack._selfPosition;
                        selfRotation = _warriorAttack._selfRotation;
                    }

                    var vectorDirection = targetPosition - selfPosition;
                    var distance = vectorDirection.magnitude;
                    if (distance > _warriorAttack._attackingDistance)
                    {
                        var destination = targetPosition - vectorDirection.normalized *
                            (_warriorAttack._attackingDistance * AtackingDistance);
                        _warriorAttack._targetPositions.OnNext(destination);
                        Thread.Sleep(SleepThreadTime);
                    }
                    else if (selfRotation != Quaternion.LookRotation(vectorDirection))
                    {
                        _warriorAttack
                            ._targetRotations
                            .OnNext(Quaternion.LookRotation(vectorDirection));
                    }
                    else
                    {
                        _warriorAttack._attackTargets.OnNext(_target);
                        Thread.Sleep(SleepThreadTime);
                    }
                }
            }

            public IAwaiter<AsyncExtensions.Void> GetAwaiter()
            {
                return new AttackOperationAwaiter(this);
            }
        }

        public float Damage => _damage;
        public event Action OnComplete;
        [SerializeField] private float _damage;
        [SerializeField] private Animator _animator;
        [SerializeField] private WarriorStop _stopCommand;
        [Inject] private IHealthHolder _selfHealth;
        [Inject(Id = "AttackDistance")] private float _attackingDistance;
        private Vector3 _selfPosition;
        private Vector3 _targetPosition;
        private Quaternion _selfRotation;
        private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
        private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
        private readonly Subject<ICanAttacked> _attackTargets = new Subject<ICanAttacked>();
        private Transform _targetTransform;
        private AttackOperation _attackOperation;
        private NavMeshAgent _navAgent;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }

        [Inject]
        private void Init()
        {
            _targetPositions
                .Select(value => new Vector3((float) Math.Round(value.x, 2), (float) Math.Round(value.x, 2),
                    (float) Math.Round(value.x, 2)))
                .Distinct()
                .ObserveOnMainThread()
                .Subscribe(MoveToTarget);
            _attackTargets
                .ObserveOnMainThread()
                .Subscribe(AttackTarget);
            _targetRotations
                .ObserveOnMainThread()
                .Subscribe(AttackRotation);
        }

        private void AttackRotation(Quaternion targetRotation)
        {
            transform.rotation = targetRotation;
        }

        private void MoveToTarget(Vector3 target)
        {
            _navAgent.destination = target;
            _animator.SetTrigger(AnimationRun);
        }

        private void AttackTarget(ICanAttacked target)
        {
            _navAgent.isStopped = true;
            _navAgent.ResetPath();
            _animator.SetTrigger(AnimationAttack);
            target.ReceivedDamage(GetComponent<IDamageDealer>().Damage);
        }

        public override async Task ExecuteSpecificCommand(IAttackCommand command)
        {
            _targetTransform = (command.Attacked as Component).transform;
            _attackOperation = new AttackOperation(this, command.Attacked);
            Update();
            _stopCommand.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _attackOperation.WithCancellation(_stopCommand.CancellationTokenSource.Token);
            }
            catch (Exception e)
            {
                _attackOperation.Cancel();
            }
            _animator.SetTrigger(AnimationIdle);
            _attackOperation = null;
            _targetTransform = null;
            _stopCommand.CancellationTokenSource = null;
        }

        private void Update()
        {
            if (_attackOperation == null)
            {
                return;
            }

            lock(this)
            {
                _selfPosition = transform.position;
                _selfRotation = transform.rotation;
                if (_targetTransform  != null)
                {
                    _targetPosition = _targetTransform .position;
                }
            }
        }
    }
}