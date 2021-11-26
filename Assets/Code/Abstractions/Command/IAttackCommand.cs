namespace Code.Abstractions.Command
{
    public interface IAttackCommand:ICommand
    {
        ICanAttacked Attacked { get; }
    }
}