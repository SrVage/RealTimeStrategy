using System;
using Code.Abstractions.Command;
using Code.ControlSystem.Command;
using Code.ControlSystem.Scriptable;
using Code.Tools.InjectAssetAttribute;
using Code.Tools.Scriptable;
using UnityEngine;
using Zenject;

namespace Code.UI.UIModel
{
    public class MoveCommandCreator:CommandCreatorBase<IMoveCommand>
    {
        [Inject] private AssetsContext _context;
        public event Action<IMoveCommand> callback;

        [Inject]
        public void Init(GroundPointValue point)
        {
            point.OnSelected += SetTarget;
        }

        public void SetTarget(Vector3 target)
        {
            callback?.Invoke(_context.Inject(new MoveCommand(target)));
            callback = null;
        }
        protected override void classSpecificCommandCreator(Action<IMoveCommand> creationCallback)
        {
            callback = creationCallback;
        }
    }
}