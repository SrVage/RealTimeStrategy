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
        [Inject] private DiContainer _container;
        protected override void ClassSpecificCommandCreator(Action<IProduceUnitCommand> creationCallback)
        {
            var produceUnitCommand = _assetsContext.Inject(new ProduceUnitCommandHier());
            _container.Inject(produceUnitCommand);
            creationCallback?.Invoke(produceUnitCommand);
            
        }
    }
}