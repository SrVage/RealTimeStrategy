using UnityEngine;

namespace Code.Abstractions.Command
{
    public interface IMoveCommand:ICommand
    {
        public void Move();
    }
}