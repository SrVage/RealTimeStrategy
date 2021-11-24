using Code.Abstractions.Command;
using UnityEngine;

namespace Code.Core.Buildings
{
    public class SecondBuilding:CommandExecutorBase<IProduceUnitCommand>
    {
        private float _maxHealth=1000;
        private int _count = 0;
        
        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            Instantiate(command.UnitPrefab, transform.position, Quaternion.identity);
        }
    }
}