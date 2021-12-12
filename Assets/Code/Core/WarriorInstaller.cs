using Code.Abstractions;
using Code.Abstractions.Command;
using Zenject;

namespace Code.Core
{
    public class WarriorInstaller:MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IHealthHolder>().FromComponentInChildren();
            Container.Bind<float>().WithId("AttackDistance").FromInstance(3f);
            Container.Bind<int>().WithId("AttackPeriod").FromInstance(1400);
            Container.Bind<ICommandQueue>().FromComponentInChildren();
            Container.Bind<IFractionMember>().FromComponentInChildren();
        }
    }
}