using System.Threading;
using Code.Abstractions.Command;

namespace Code.Core.Unit
{
    public class WarriorStop:CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
        }
    }
}