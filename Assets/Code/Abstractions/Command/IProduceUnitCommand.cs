using UnityEngine;

namespace Code.Abstractions.Command
{
    public interface IProduceUnitCommand:ICommand
    {
        GameObject UnitPrefab { get; }
    }
}