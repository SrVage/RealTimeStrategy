using Code.Abstractions.Command;
using UnityEngine;

namespace Code.ControlSystem.Command
{
    public class PatrolCommand:IPatrolCommand
    {
        public PatrolCommand(Vector3 target, Vector3 current)
        {
            Target = target;
            Current = current;
        }

        public Vector3 Target { get; }
        public Vector3 Current { get; }
    }
}