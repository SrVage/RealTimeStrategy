using Code.Abstractions.Command;
using UnityEngine;

namespace Code.Core.Buildings
{
    public class UnitProductionTask:IUnitProductionTask
    {
        public Sprite Icon { get; }
        public string Name { get; }
        public float TimeLeft { get; set; }
        public float ProductionTime { get; }
        public GameObject UnitPrefab { get; }

        public UnitProductionTask(Sprite icon, string name, float timeLeft, GameObject unitPrefab)
        {
            Icon = icon;
            Name = name;
            TimeLeft = timeLeft;
            ProductionTime = timeLeft;
            UnitPrefab = unitPrefab;
        }
    }
}