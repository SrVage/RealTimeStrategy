using Code.Abstractions.Command;
using UnityEngine;

namespace Code.ControlSystem.Command
{
    public class PatrolCommand:IPatrolCommand
    {
        public void Patrol()
        {
            Debug.Log("Patrol");
        }
    }
}