using System.Threading;
using System.Threading.Tasks;
using Code.Abstractions.Command;

namespace Code.Core.CommandExecutors
{
    public class StopCommandExecutor:CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public override async Task ExecuteSpecificCommand(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
        }
    }
}