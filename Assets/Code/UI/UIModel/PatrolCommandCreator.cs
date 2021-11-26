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
    public class PatrolCommandCreator:CommandCreatorBase<IPatrolCommand>
    {
        [Inject] private AssetsContext _context;
        public event Action<IPatrolCommand> callback;

        [Inject]
        public void Init(GroundPointValue point)
        {
            point.OnSelected += SetTarget;
        }

        public void SetTarget(Vector3 target)
        {
            callback?.Invoke(_context.Inject(new PatrolCommand(target)));
            callback = null;
        }
        protected override void classSpecificCommandCreator(Action<IPatrolCommand> creationCallback)
        {
            callback = creationCallback;
        }
    }
}