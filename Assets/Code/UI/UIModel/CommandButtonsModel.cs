using System;
using Code.Abstractions.Command;
using UnityEngine;
using Zenject;

namespace Code.UI.UIModel
{
    public class CommandButtonsModel
    {
        public event Action<ICommandExecutor> OnCommandAccepted;
        public event Action OnCommandSent;
        public event Action OnCommandCanceled;
        [Inject] private CommandCreatorBase<IMoveCommand> _mover;
        [Inject] private CommandCreatorBase<IAttackCommand> _attacker;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patroller;
        [Inject] private CommandCreatorBase<IProduceUnitCommand> _creator;
        [Inject] private CommandCreatorBase<IStopCommand> _stopper;
        [Inject] private CommandCreatorBase<IProduceTarget> _targetter;
        private bool _commandIsPending;
        

        public void OnCommandButtonClicked(ICommandExecutor commandExecutor, ICommandQueue commandQueue)
        {
            if (_commandIsPending)
                ProcessOnCancelled();
            _commandIsPending = true;
            OnCommandAccepted?.Invoke(commandExecutor);
            _creator.ProcessCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandQueue, command));
            _mover.ProcessCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandQueue, command));
            _attacker.ProcessCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandQueue, command));
            _patroller.ProcessCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandQueue, command));
            _stopper.ProcessCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandQueue, command));
            _targetter.ProcessCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandQueue, command));
        }
        
        public void ExecuteCommandWrapper(ICommandQueue commandQueue, object command)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                commandQueue.Clear();
            }
            commandQueue.EnqueueCommand(command);
            _commandIsPending = false;
            OnCommandSent?.Invoke();
        }

        public void OnSelectionChanged()
        {
            _commandIsPending = false;
            ProcessOnCancelled();
        }
        
        private void ProcessOnCancelled()
        {
            _attacker.ProcessCancel();
            _creator.ProcessCancel();
            _mover.ProcessCancel();
            _patroller.ProcessCancel();
            _stopper.ProcessCancel();
            
            OnCommandCanceled?.Invoke();
        }
    }
}