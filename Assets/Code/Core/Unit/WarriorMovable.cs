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
    [RequireComponent(typeof(Outline))]
    public class WarriorMovable:CommandExecutorBase<IMoveCommand>, IChangeAnimation
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private WarriorMovementStop _stop;
        [SerializeField] private WarriorStop _warriorStop;
        [SerializeField] private WarriorAnimation _warriorAnimation;
        public Action<AnimationStates> ChangeAnimation { get; set; }

        [Inject]
        private void Init()
        {
            _warriorAnimation.Init(this);
        }

        public override async Task ExecuteSpecificCommand(IMoveCommand command)
        {
            _navMeshAgent.destination = command.Target;
            ChangeAnimation?.Invoke(AnimationStates.Run);
            _warriorStop.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _stop.WithCancellation(_warriorStop.CancellationTokenSource.Token);
            }
            catch (Exception e)
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.ResetPath();
            }
            Debug.Log("end");
            //_stop.StopMove();
            _warriorStop.CancellationTokenSource = null;
            ChangeAnimation?.Invoke(AnimationStates.Idle);
        }
    }
}