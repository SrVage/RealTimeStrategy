using Code.Abstractions.Command;

namespace Code.Core.Unit
{
    public class WarriorStop:CommandExecutorBase<IStopCommand>
    {
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            command.Stop();
        }
    }
}