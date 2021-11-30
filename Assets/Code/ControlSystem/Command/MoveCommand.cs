using Code.Abstractions.Command;
using Code.ControlSystem.Scriptable;
using UnityEngine;
using Zenject;

namespace Code.ControlSystem.Command
{
    public class MoveCommand:IMoveCommand
    {
        public MoveCommand(Vector3 target)
        {
            this.Target = target;
            Debug.Log($"Move {target}");
        }

        public Vector3 Target { get; }
    }
}