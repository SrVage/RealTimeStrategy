using Zenject;

namespace Code.UI.UIView
{
    public class UIViewInstaller:MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<QueueProduceView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}