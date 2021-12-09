using UniRx;

namespace Code.Abstractions.Command
{
    public interface IMoveQueueCommand
    {
        IReadOnlyReactiveCollection<IMoveCommand> Queue { get; }
    }
}