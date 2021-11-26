using UnityEngine;

namespace Code.Abstractions.Command
{
    public interface IMoveCommand:ICommand
    {
        public Vector3 Target { get; }
    }
}