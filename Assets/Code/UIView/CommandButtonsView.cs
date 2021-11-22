using System;
using System.Collections.Generic;
using System.Linq;
using Code.Abstractions.Command;
using UnityEngine;
using UnityEngine.UI;

namespace Code.ControlSystem
{
    public class CommandButtonsView:MonoBehaviour
    {
        public Action<ICommandExecutor> OnClick;
        [SerializeField] private GameObject _buttonAttack;
        [SerializeField] private GameObject _buttonProduce;
        [SerializeField] private GameObject _buttonMove;
        [SerializeField] private GameObject _buttonPatrol;
        [SerializeField] private GameObject _buttonCancel;

        private Dictionary<Type, GameObject> _executorTypesButton;

        private void Start()
        {
            _executorTypesButton = new Dictionary<Type, GameObject>();
            _executorTypesButton.Add(typeof(CommandExecutorBase<IAttackCommand>), _buttonAttack);
            _executorTypesButton.Add(typeof(CommandExecutorBase<IProduceUnitCommand>), _buttonProduce);
            _executorTypesButton.Add(typeof(CommandExecutorBase<IMoveCommand>), _buttonMove);
            _executorTypesButton.Add(typeof(CommandExecutorBase<IPatrolCommand>), _buttonPatrol);
            _executorTypesButton.Add(typeof(CommandExecutorBase<IStopCommand>), _buttonCancel);
        }

        public void MakeButton(IEnumerable<ICommandExecutor> commandExecutors)
        {
            foreach (var commandExecutor in commandExecutors)
            {
                var button = _executorTypesButton
                    .First(type => type.Key.IsInstanceOfType(commandExecutor)).Value;
                button.SetActive(true);
                button.GetComponent<Button>().onClick.AddListener(()=>OnClick?.Invoke(commandExecutor));
            }
        }

        public void Clear()
        {
            foreach (var button in _executorTypesButton)
            {
                button.Value.GetComponent<Button>().onClick.RemoveAllListeners();
                button.Value.SetActive(false);
            }
        }
    }
}