namespace Code.Abstractions.Command
{
    public interface IUnitProductionTask:IIconHolder
    {
        string Name { get; }
        float TimeLeft { get; }
        float ProductionTime { get; }
    }
}