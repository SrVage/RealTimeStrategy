using Code.Abstractions;
using Code.Abstractions.Command;
using Code.ControlSystem.Command;
using UnityEngine;
using Zenject;

namespace Code.Core
{
    public class CanAttacked:MonoBehaviour, ICanAttacked
    {
        private int DeathConst = Animator.StringToHash("Death");
        [SerializeField] private CommandExecutorBase<IStopCommand> _commandExecutor;
        [SerializeField] private Animator _animator;
        public Transform Transform => transform;
        
        private void Awake()
        {
            Health = 100;
        }
        public void ReceivedDamage(float amount)
        {
            Debug.Log(Health);
            if (Health <= 0)
                return;
            Health -= amount;
            if (Health <= 0)
            {
                _animator.SetTrigger(DeathConst);
                Invoke(nameof(Death), 1f);
            }
        }

        private void Death()
        {
            _commandExecutor.ExecuteSpecificCommand(new StopCommand());
            Destroy(gameObject);
        }

        public string Name { get; }
        public float Health { get; private set; }
        public float MaxHealth { get; }
    }
}