using System;
using System.Threading;
using Code.Abstractions;
using Code.Abstractions.Command;
using Code.Tools;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Core.Unit
{
    [RequireComponent(typeof(Outline))]
    public class WarriorMovable:CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private WarriorMovementStop _stop;
        [SerializeField] private WarriorStop _warriorStop;
        public event Action<AnimationStates> ChangeStates; 
        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            _navMeshAgent.destination = command.Target;
            ChangeStates?.Invoke(AnimationStates.Run);
            _warriorStop.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _stop.WithCancellation(_warriorStop.CancellationTokenSource.Token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            _warriorStop.CancellationTokenSource = null;
            ChangeStates?.Invoke(AnimationStates.Idle);
        }
    }
}