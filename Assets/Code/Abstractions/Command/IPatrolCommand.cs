using UnityEngine;

namespace Code.Abstractions.Command
{
    public interface IPatrolCommand:ICommand
    {
        public Vector3 Target { get; }
        public Vector3 Current { get; }
    }
}