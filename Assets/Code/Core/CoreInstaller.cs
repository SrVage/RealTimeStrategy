using Zenject;

namespace Code.Core
{
    public class CoreInstaller:MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
        }
    }
}