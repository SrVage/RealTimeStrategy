using System.Collections.Generic;
using Code.Abstractions;
using Code.Abstractions.Command;
using Code.ControlSystem;
using Code.ControlSystem.Command;
using Code.ControlSystem.Scriptable;
using Code.Tools.InjectAssetAttribute;
using Code.UI.UIModel;
using Code.UI.UIView;
using UnityEngine;
using Zenject;

namespace Code.UI.UIPresenter
{
    public class CommandButtonPresenter:MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;
        
        [Inject] private CommandButtonsModel _model;
        
        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectable.OnSelected += onSelected;
            onSelected(_selectable.CurrentValue);
            _view.OnClick += _model.OnCommandButtonClicked;
            _model.OnCommandSent += _view.UnblockAllButton;
            _model.OnCommandCanceled += _view.UnblockAllButton;
            _model.OnCommandAccepted += _view.BlockButton;
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
    }
}