using Code.Abstractions.Command;
using UnityEngine;

namespace Code.ControlSystem.Command
{
    public class ProduceTargetCommand:IProduceTarget
    {
        public ProduceTargetCommand(Vector3 point)
        {
            Debug.Log("target");
            Target = point;
        }
        public Vector3 Target { get; private set; }
    }
}