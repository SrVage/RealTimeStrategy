using System;
using Code.Abstractions.Command;
using UnityEngine;

namespace Code.UI.UIModel
{
    public abstract class CommandCreatorBase<T> where T: ICommand
    {
        public ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
        {
            if (commandExecutor is ICommandExecutor<T> specificExecutor)
            {
                ClassSpecificCommandCreator(callback);
            }
            return commandExecutor;
        }
        protected abstract void ClassSpecificCommandCreator(Action<T> creationCallback);

        public virtual void ProcessCancel()
        {
            
        }
    }
}