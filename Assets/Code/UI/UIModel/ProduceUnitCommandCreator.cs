using System;
using Code.Abstractions.Command;
using Code.ControlSystem.Command;
using Code.Tools.InjectAssetAttribute;
using Code.Tools.Scriptable;
using Zenject;

namespace Code.UI.UIModel
{
    public class ProduceUnitCommandCreator:CommandCreatorBase<IProduceUnitCommand>
    {
        [Inject] private AssetsContext _assetsContext;
        protected override void classSpecificCommandCreator(Action<IProduceUnitCommand> creationCallback)
        {
            creationCallback?.Invoke(_assetsContext.Inject(new ProduceUnitCommandHier()));
        }
    }
}