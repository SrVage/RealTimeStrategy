using System;
using Code.Abstractions;
using UnityEngine;

namespace Code.Core.Unit
{
    public class WarriorAnimation:MonoBehaviour
    {
        private readonly int RunAnimation = Animator.StringToHash("Run");
        private readonly int IdleAnimation = Animator.StringToHash("Idle");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private WarriorMovable _warriorMovable;

        public void Init(IChangeAnimation changer)
        {
            changer.ChangeAnimation += ChangeAnimation;
        }

        private void ChangeAnimation(AnimationStates states)
        {
            switch (states)
            {
                case AnimationStates.Idle:
                    _animator.SetTrigger(IdleAnimation);
                    break;
                case AnimationStates.Run:
                    _animator.SetTrigger(RunAnimation);
                    break;
                case AnimationStates.Attack:
                    break;
                case AnimationStates.Hit:
                    break;
                case AnimationStates.Death:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(states), states, null);
            }
        }
    }
}