using System;
using Code.Abstractions.Command;
using UnityEngine;

namespace Code.UI.UIModel
{
    public abstract class CommandCreatorBase<T> where T:ICommand
    {
        public ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
        {
            var classSpecificCommandExecutor = commandExecutor as CommandExecutorBase<T>;
            if (classSpecificCommandExecutor != null)
            {
                classSpecificCommandCreator(callback);
            }
            return commandExecutor;
        }
        protected abstract void classSpecificCommandCreator(Action<T> creationCallback);

        public virtual void ProcessCancel()
        {
            
        }
    }
}