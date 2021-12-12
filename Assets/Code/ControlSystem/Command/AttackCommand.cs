using Code.Abstractions;
using Code.Abstractions.Command;
using UnityEngine;

namespace Code.ControlSystem.Command
{
    public class AttackCommand:IAttackCommand
    {
        public ICanAttacked Attacked { get; }
        public AttackCommand(ICanAttacked attacked)
        {
            Attacked = attacked;
        }
    }
}