namespace Code.Abstractions.Command
{
    public interface ICommandExecutor
    {
        void ExecuteCommand(object command);
    }
}