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
        [SerializeField] private CommandButtonsView _view;
        
        [Inject] private CommandButtonsModel _model;
        
        private ISelectable _currentSelectable;
        private ICommandExecutor _move;

        private void Start()
        {
            _selectable.Subscribe(onSelected);
            _view.OnClick += _model.OnCommandButtonClicked;
            _model.OnCommandSent += _view.UnblockAllButton;
            _model.OnCommandCanceled += _view.UnblockAllButton;
            _model.OnCommandAccepted += _view.BlockButton;
        }

        private void onSelected(ISelectable selectable)
        { 
            if (_currentSelectable == selectable)
                return;
            if (_currentSelectable!=null)
                _model.OnSelectionChanged();
            _currentSelectable = selectable;
            _view.Clear();

            if (selectable != null)
            {
                var listExecutor = new List<ICommandExecutor>();
                listExecutor.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                var queue = (selectable as Component).GetComponentInParent<ICommandQueue>();
                _view.MakeButton(listExecutor, queue);
            }
        }
    }
}