using System;
using System.Collections.Generic;
using System.Linq;
using Code.Abstractions;
using Code.Abstractions.Command;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.UIView
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
                button.GetComponent<Button>().OnClickAsObservable().Subscribe(_ => OnClick.Invoke(commandExecutor));
            }
        }
        
        public void BlockButton(ICommandExecutor commandExecutor)
        {
            UnblockAllButton();
            getButtonByType(commandExecutor.GetType()).GetComponent<Selectable>().interactable = false;
        }

        public void UnblockAllButton() => SetInteract(true);

        private void SetInteract(bool value)
        {
            _buttonAttack.GetComponent<Selectable>().interactable = value;
            _buttonCancel.GetComponent<Selectable>().interactable = value;
            _buttonMove.GetComponent<Selectable>().interactable = value;
            _buttonPatrol.GetComponent<Selectable>().interactable = value;
            _buttonProduce.GetComponent<Selectable>().interactable = value;
        }

        public void Clear()
        {
            foreach (var button in _executorTypesButton)
            {
                button.Value.GetComponent<Button>().onClick.RemoveAllListeners();
                button.Value.SetActive(false);
            }
        }

        private GameObject getButtonByType(Type executorInstanceType)
        {
            return _executorTypesButton
                .First(type => type.Key.IsAssignableFrom(executorInstanceType))
                .Value;
        }
    }
}