using Code.Abstractions.Command;
using Code.Tools.InjectAssetAttribute;
using UnityEngine;

namespace Code.ControlSystem.Command
{
    public class ProduceUnitCommand:IProduceUnitCommand
    {
        public GameObject UnitPrefab => _unitPrefab;
        [InjectAsset("FootmanPBR")] protected GameObject _unitPrefab;
    }
    
}