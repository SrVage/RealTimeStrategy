using Code.Abstractions.Command;

namespace Code.Core.Unit
{
    public class WarriorPatrol:CommandExecutorBase<IPatrolCommand>
    {
        public override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            command.Patrol();
        }
    }
}