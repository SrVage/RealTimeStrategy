using System;
using Code.Abstractions.Command;

namespace Code.UI.UIModel
{
    public class MoveCommandCreator:CommandCreatorBase<IMoveCommand>
    {
        protected override void classSpecificCommandCreator(Action<IMoveCommand> creationCallback)
        {
            
        }
    }
}