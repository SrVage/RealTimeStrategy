using Code.Abstractions.Command;
using UnityEngine;

namespace Code.ControlSystem.Command
{
    public class MoveCommand:IMoveCommand
    {
        public void Move(Vector3 target)
        {
            Debug.Log($"Move {target}");
        }

        public Vector3 tagret { get; }
    }
}