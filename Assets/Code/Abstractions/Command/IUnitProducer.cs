using UniRx;

namespace Code.Abstractions.Command
{
    public interface IUnitProducer
    {
        IReadOnlyReactiveCollection<IUnitProductionTask> Queue {get;}
        void Cancel(int index);
    }
}