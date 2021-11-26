using Code.Abstractions.Command;
using UnityEngine;

namespace Code.ControlSystem.Command
{
    public class PatrolCommand:IPatrolCommand
    {
        public PatrolCommand(Vector3 target)
        {
            Target = target;
            Debug.Log("Patrol"+target);
        }

        public Vector3 Target { get; }
    }
}