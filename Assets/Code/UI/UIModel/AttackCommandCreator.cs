using Code.Abstractions;
using Code.Abstractions.Command;
using Code.ControlSystem.Command;

namespace Code.UI.UIModel
{
    public class AttackCommandCreator:CancelableCommandCreatorBase<IAttackCommand, ICanAttacked>
    {
        protected override IAttackCommand CreateCommand(ICanAttacked argument) => 
            new AttackCommand(argument);
    }
}