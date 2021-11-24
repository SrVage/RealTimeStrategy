using Code.Abstractions;
using Code.Abstractions.Command;
using Code.Tools;
using UnityEngine;

namespace Code.Core.Unit
{
    [RequireComponent(typeof(Outline))]
    public class WarriorMovable:CommandExecutorBase<IMoveCommand>
    {
        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            
        }
    }
}