using System;
using System.Collections.Generic;
using Code.Abstractions;
using Code.Abstractions.Command;
using Code.ControlSystem;
using Code.ControlSystem.Command;
using Code.Tools.InjectAssetAttribute;
using UnityEngine;

namespace DefaultNamespace
{
    public class CommandButtonPresenter:MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;
        [SerializeField] private AssetsContext _context;
        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectable.OnSelected += onSelected;
            onSelected(_selectable.CurrentValue);
            _view.OnClick += onButtonClick;
        }

        private void onSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
                return;
            _currentSelectable = selectable;
            _view.Clear();

            if (selectable != null)
            {
                var listExecutor = new List<ICommandExecutor>();
                listExecutor.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeButton(listExecutor);
            }
        }

        private void onButtonClick(ICommandExecutor commandExecutor)
        {
            var unitProducer = commandExecutor as CommandExecutorBase<IProduceUnitCommand>;
            if (unitProducer != null) 
                unitProducer.ExecuteSpecificCommand(_context.Inject(new ProduceUnitCommandHier()));
            var unitAttack = commandExecutor as CommandExecutorBase<IAttackCommand>;
            if (unitAttack != null)
                unitAttack.ExecuteSpecificCommand(new AttackCommand());
            var unitMove = commandExecutor as CommandExecutorBase<IMoveCommand>;
            if (unitMove!=null)
                unitMove.ExecuteSpecificCommand(new MoveCommand());
            var unitPatrol = commandExecutor as CommandExecutorBase<IPatrolCommand>;
            if (unitPatrol!=null)
                unitPatrol.ExecuteSpecificCommand(new PatrolCommand());
            var unitStop = commandExecutor as CommandExecutorBase<IStopCommand>;
            if (unitStop!=null)
                unitStop.ExecuteSpecificCommand(new StopCommand());
        }
    }
}