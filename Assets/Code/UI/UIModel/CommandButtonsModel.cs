using System;
using Code.Abstractions.Command;
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
        private bool _commandIsPending;

        public void OnCommandButtonClicked(ICommandExecutor commandExecutor)
        {
            if (_commandIsPending)
                ProcessOnCancelled();
            _commandIsPending = true;
            OnCommandAccepted?.Invoke(commandExecutor);
            _creator.ProcessCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandExecutor, command));
            _mover.ProcessCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandExecutor, command));
            _attacker.ProcessCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandExecutor, command));
            _patroller.ProcessCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandExecutor, command));
            _stopper.ProcessCommandExecutor(commandExecutor,
                command => ExecuteCommandWrapper(commandExecutor, command));
        }
        
        public void ExecuteCommandWrapper(ICommandExecutor executor, object command)
        {
            executor.ExecuteCommand(command);
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