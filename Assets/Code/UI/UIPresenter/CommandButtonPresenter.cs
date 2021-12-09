using System;
using System.Collections.Generic;
using Code.Abstractions;
using Code.Abstractions.Command;
using Code.UI.UIModel;
using Code.UI.UIView;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.UI.UIPresenter
{
    public class CommandButtonPresenter:MonoBehaviour
    {
        [Inject] private IObservable<ISelectable> _selectable;
        [Inject] private IAwaitable<Vector3> _groundPoint;
        [SerializeField] private CommandButtonsView _view;
        
        [Inject] private CommandButtonsModel _model;
        
        private ISelectable _currentSelectable;
        private ICommandExecutor _move;

        private void Start()
        {
            _selectable.Subscribe(onSelected);
            //_groundPoint.Subscribe(Move);
            _view.OnClick += _model.OnCommandButtonClicked;
            _model.OnCommandSent += _view.UnblockAllButton;
            _model.OnCommandCanceled += _view.UnblockAllButton;
            _model.OnCommandAccepted += _view.BlockButton;
        }

        private void Move(Vector3 obj)
        {
            if (_currentSelectable != null)
            {
                _model.Move(_move);
            }
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
                foreach (var VARIABLE in listExecutor)
                {
                    if (VARIABLE is CommandExecutorBase<IMoveCommand>)
                    {
                        _move = VARIABLE;
                        break;
                    }
                    _move = null;
                }
            }
        }
    }
}