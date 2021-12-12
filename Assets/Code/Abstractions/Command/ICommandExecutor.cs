namespace Code.Abstractions.Command
{
    public interface ICommandExecutor
    {
        
    }
    
    public interface ICommandExecutor<T>:ICommandExecutor where T:ICommand
    {}
}