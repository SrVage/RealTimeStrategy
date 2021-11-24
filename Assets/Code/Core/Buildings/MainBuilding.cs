using Code.Abstractions.Command;
using UnityEngine;

namespace Code.Core.Buildings
{
    public class MainBuilding:CommandExecutorBase<IProduceUnitCommand>
    {
        [SerializeField] private Transform _unitParent;
        private float _maxHealth=1000;
        private int _count = 0;
        
        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            Vector3 offset = new Vector3(2+_count%5, 0, _count/5);
            Instantiate(command.UnitPrefab, transform.position + offset, Quaternion.identity, _unitParent);
            _count++;
        }
    }
}