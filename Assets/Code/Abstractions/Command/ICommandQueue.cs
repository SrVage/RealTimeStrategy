namespace Code.Abstractions.Command
{
    public interface ICommandQueue
    {
        ICommand CurrentCommand { get; }
        void EnqueueCommand(object command);
        void Clear();
    }
}