using System;
using Code.Abstractions.Command;
using Code.ControlSystem.Command;

namespace Code.UI.UIModel
{
    public class StopCommandCreator:CommandCreatorBase<IStopCommand>
    {
        protected override void classSpecificCommandCreator(Action<IStopCommand> creationCallback) 
            => creationCallback?.Invoke(new StopCommand());
    }
}