using UnityEngine;

namespace Code.Abstractions.Command
{
    public interface IProduceUnitCommand:ICommand, IIconHolder
    {
        string Name { get; }
        GameObject UnitPrefab { get; }
        float ProductionTime { get; }
    }
}