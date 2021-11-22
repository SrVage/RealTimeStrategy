using Code.Abstractions.Command;
using UnityEngine;

namespace Code.ControlSystem.Command
{
    public class MoveCommand:IMoveCommand
    {
        public void Move()
        {
            Debug.Log("Move");
        }
    }
}