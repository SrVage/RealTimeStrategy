using Code.Abstractions.Command;
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
            Container.Bind<CommandCreatorBase<IProduceTarget>>().To<ProduceTargetCommandCreator>().AsTransient();

            Container.Bind<CommandButtonsModel>().AsTransient();

            Container.Bind<float>().WithId("FootmanPBR").FromInstance(3f);
            Container.Bind<string>().WithId("FootmanPBR").FromInstance("FootmanPBR");
            Container.Bind<QueueProduceModel>().AsSingle();
        }
    }
}