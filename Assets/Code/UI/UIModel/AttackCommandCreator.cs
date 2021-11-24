using System;
using Code.Abstractions.Command;

namespace Code.UI.UIModel
{
    public class AttackCommandCreator:CommandCreatorBase<IAttackCommand>
    {
        protected override void classSpecificCommandCreator(Action<IAttackCommand> creationCallback)
        {
            
        }
    }
}