using Code.Abstractions.Command;
using UnityEngine;

namespace Code.ControlSystem.Command
{
    public class ProduceUnitCommand:IProduceUnitCommand
    {
        public GameObject UnitPrefab => _unitPrefab;
        [SerializeField] private GameObject _unitPrefab;
    }
}