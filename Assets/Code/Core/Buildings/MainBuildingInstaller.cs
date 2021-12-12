using Code.Abstractions;
using Zenject;

namespace Code.Core.Buildings
{
    public class MainBuildingInstaller:MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFractionMember>().FromComponentInChildren();
        }
    }
}