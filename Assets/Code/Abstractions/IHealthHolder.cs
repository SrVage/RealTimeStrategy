namespace Code.Abstractions
{
    public interface IHealthHolder
    {
        string Name { get; }
        float Health { get; }
        float MaxHealth { get; }
    }
}