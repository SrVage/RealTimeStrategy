using System;
using Code.Abstractions.Command;

namespace Code.UI.UIModel
{
    public class StopCommandCreator:CommandCreatorBase<IStopCommand>
    {
        protected override void classSpecificCommandCreator(Action<IStopCommand> creationCallback)
        {
            
        }
    }
}