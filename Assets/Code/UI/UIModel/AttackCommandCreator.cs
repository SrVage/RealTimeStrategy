using System;
using Code.Abstractions;
using Code.Abstractions.Command;
using Code.ControlSystem.Command;
using Code.ControlSystem.Scriptable;
using Code.Tools.InjectAssetAttribute;
using Code.Tools.Scriptable;
using Zenject;

namespace Code.UI.UIModel
{
    public class AttackCommandCreator:CommandCreatorBase<IAttackCommand>
    {
        private event Action<IAttackCommand> callback; 
        [Inject] private AssetsContext _context;
        [Inject]
        public void Init(AttackedValue attackedValue)
        {
            attackedValue.OnSelected += onSelected;
        }

        private void onSelected(ICanAttacked attacked)
        {
            callback?.Invoke(_context.Inject(new AttackCommand(attacked)));
        }
        protected override void classSpecificCommandCreator(Action<IAttackCommand> creationCallback)
        {
            callback = creationCallback;
        }
    }
}