using Code.Abstractions.Command;
using Code.ControlSystem.Command;
using Code.Tools.InjectAssetAttribute;
using Code.Tools.Scriptable;
using UnityEngine;
using Zenject;

namespace Code.UI.UIModel
{
    public class UIModelInstaller:MonoInstaller
    {
        [SerializeField] private AssetsContext _context;

        public override void InstallBindings()
        {
            Container.Bind<AssetsContext>().FromInstance(_context);

            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCreator>().AsTransient();

            Container.Bind<CommandButtonsModel>().AsTransient();
        }
    }
}