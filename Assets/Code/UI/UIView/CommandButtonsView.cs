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
        public Action<ICommandExecutor, ICommandQueue> OnClick;
        [SerializeField] private GameObject _buttonAttack;
        [SerializeField] private GameObject _buttonProduce;
        [SerializeField] private GameObject _buttonMove;
        [SerializeField] private GameObject _buttonPatrol;
        [SerializeField] private GameObject _buttonCancel;
        [SerializeField] private GameObject _buttonTarget;

        private Dictionary<Type, GameObject> _executorTypesButton;

        private void Start()
        {
            _executorTypesButton = new Dictionary<Type, GameObject>();
            _executorTypesButton.Add(typeof(ICommandExecutor<IAttackCommand>), _buttonAttack);
            _executorTypesButton.Add(typeof(ICommandExecutor<IProduceUnitCommand>), _buttonProduce);
            _executorTypesButton.Add(typeof(ICommandExecutor<IMoveCommand>), _buttonMove);
            _executorTypesButton.Add(typeof(ICommandExecutor<IPatrolCommand>), _buttonPatrol);
            _executorTypesButton.Add(typeof(ICommandExecutor<IStopCommand>), _buttonCancel);
            _executorTypesButton.Add(typeof(ICommandExecutor<IProduceTarget>), _buttonTarget);
        }

        public void MakeButton(IEnumerable<ICommandExecutor> commandExecutors, ICommandQueue queue)
        {
            foreach (var commandExecutor in commandExecutors)
            {
                var button = _executorTypesButton
                    .First(type => type.Key.IsInstanceOfType(commandExecutor)).Value;
                button.SetActive(true);
                button.GetComponent<Button>().OnClickAsObservable().Subscribe(_ => OnClick.Invoke(commandExecutor, queue));
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
            _buttonTarget.GetComponent<Selectable>().interactable = value;
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