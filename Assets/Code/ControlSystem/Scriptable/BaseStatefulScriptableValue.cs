using System;
using Code.Abstractions.Command;
using UniRx;

namespace Code.ControlSystem.Scriptable
{
    public class BaseStatefulScriptableValue<T>:BaseScriptableValue<T>, IObservable<T>
    {
        public override IAwaiter<T> GetAwaiter() => 
            new NewValueNotifier<T>(this, Result);

        public ReactiveProperty<T> Result = new ReactiveProperty<T>();
        public override void SetValue(T selectable)
        {
            Result.Value = selectable;
        }

        public IDisposable Subscribe(IObserver<T> observer) => 
            Result.Subscribe(observer);
    }
}