using System;
using Code.Abstractions.Command;
using Code.ControlSystem.Command;
using Code.Tools;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Core.Unit
{
    public class WarriorCommandQueue:MonoBehaviour, ICommandQueue
    {
        public ICommand CurrentCommand => _commandQueue.Count > 0 ? _commandQueue[0] : default;
        
        [Inject] private CommandExecutorBase<IMoveCommand> _moveCommand;
        [Inject] private CommandExecutorBase<IPatrolCommand> _patrolCommand;
        [Inject] private CommandExecutorBase<IAttackCommand> _attackCommand;
        [Inject] private CommandExecutorBase<IStopCommand> _stopCommand;

        private readonly ReactiveCollection<ICommand> _commandQueue = new ReactiveCollection<ICommand>();

        [Inject]
        private void Init()
        {
            _commandQueue.ObserveAdd().Subscribe(AddCommand).AddTo(this);
        }


        private void AddCommand(ICommand command, int index)
        {
            if (index==0)
                ExecuteCommand(command);
        }

        private async void ExecuteCommand(ICommand command)
        {
            await _moveCommand.TryExecuteCommand(command);
            await _patrolCommand.TryExecuteCommand(command);
            await _attackCommand.TryExecuteCommand(command);
            await _stopCommand.TryExecuteCommand(command);
            if (_commandQueue.Count>0)
                _commandQueue.RemoveAtIndex(0);
            CheckQueue();
        }

        private void CheckQueue()
        {
            if (_commandQueue.Count>0)
                ExecuteCommand(_commandQueue[0]);
        }

        public void EnqueueCommand(object command)
        {
            var com = command as ICommand;
            _commandQueue.Add(com);
        }

        public void Clear()
        {
            _commandQueue.Clear();
            _stopCommand.ExecuteSpecificCommand(new StopCommand());
        }
    }
}