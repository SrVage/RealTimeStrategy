using Code.Abstractions;
using Code.Abstractions.Command;
using Code.Tools;
using UnityEngine;

namespace Code.Core.Unit
{
    [RequireComponent(typeof(Outline))]
    public class WarriorMovable:CommandExecutorBase<IMoveCommand>, ISelectable
    {
        public string Name { get; }
        public float Health { get; }
        public float MaxHealth { get; }
        public Sprite Icon { get; }
        public void Selecting()
        {
            GetComponent<Outline>().enabled = true;
        }

        public void Unselecting()
        {
            GetComponent<Outline>().enabled = false;
        }

        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            command.Move();
        }
    }
}