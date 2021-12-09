using UnityEngine;

namespace Code.Abstractions.Command
{
    public interface IProduceTarget:ICommand
    {
        public Vector3 Target { get; }
    }
}