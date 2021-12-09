using Code.Abstractions.Command;
using Code.Tools.InjectAssetAttribute;
using UnityEngine;
using Zenject;

namespace Code.ControlSystem.Command
{
    public class ProduceUnitCommand:IProduceUnitCommand
    {
        [Inject (Id = "FootmanPBR")]public string Name { get; }
        [Inject (Id = "FootmanPBR")] public Sprite Icon { get; }
        [Inject (Id = "FootmanPBR")] public float ProductionTime { get; }
        [InjectAsset("FootmanPBR")] protected GameObject _unitPrefab;
        public GameObject UnitPrefab => _unitPrefab;
    }
    
}