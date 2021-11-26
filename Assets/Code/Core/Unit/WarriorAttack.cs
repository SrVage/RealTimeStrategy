using Code.Abstractions.Command;

namespace Code.Core.Unit
{
    public class WarriorAttack:CommandExecutorBase<IAttackCommand>
    {
        public override void ExecuteSpecificCommand(IAttackCommand command)
        {
        }
    }
}