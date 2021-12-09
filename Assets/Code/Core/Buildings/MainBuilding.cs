using System.Threading.Tasks;
using Code.Abstractions.Command;
using UnityEngine;

namespace Code.Core.Buildings
{
    public class MainBuilding:CommandExecutorBase<IProduceUnitCommand>
    {
        [SerializeField] private Transform _unitParent;
        [SerializeField] private int _productionTime=2000;
        private float _maxHealth=1000;
        private int _count = 0;
        
        public override async void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            /*await Task.Delay(_productionTime);
            Vector3 offset = new Vector3(2+_count%5, 0, _count/5);
            Instantiate(command.UnitPrefab, transform.position + offset, Quaternion.identity, _unitParent);
            _count++;*/
        }
    }
}