using System;
using Code.Abstractions.Command;

namespace Code.UI.UIModel
{
    public class PatrolCommandCreator:CommandCreatorBase<IPatrolCommand>
    {
        protected override void classSpecificCommandCreator(Action<IPatrolCommand> creationCallback)
        {
            
        }
    }
}