using Code.Abstractions.Command;
using UnityEngine;

namespace Code.ControlSystem.Command
{
    public class AttackCommand:IAttackCommand
    {
        public void Attack()
        {
            Debug.Log("Attack");
        }
    }
}