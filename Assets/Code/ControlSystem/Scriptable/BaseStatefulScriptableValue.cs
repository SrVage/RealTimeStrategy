using System;
using Code.Abstractions.Command;
using UniRx;
using UnityEngine;

namespace Code.ControlSystem.Scriptable
{
    public class BaseStatefulScriptableValue<T>:BaseScriptableValue<T>, IObservable<T>
    {
        public ReactiveProperty<T> Result = new ReactiveProperty<T>();
        public override void SetValue(T selectable)
        {
            base.SetValue(selectable);
            Result.Value = selectable;
        }

        public IDisposable Subscribe(IObserver<T> observer) => 
            Result.Subscribe(observer);
    }
}