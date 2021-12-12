using System;
using UniRx;
using UnityEngine;

namespace Code.ControlSystem.Scriptable
{
    public class BaseStatelessScriptableValue<T>:BaseScriptableValue<T>, IObservable<T>
    {
        private Subject<T> _dataSource = new Subject<T>();

        public override void SetValue(T selectable)
        {
            base.SetValue(selectable);
            _dataSource.OnNext(selectable);
        }

        public IDisposable Subscribe(IObserver<T> observer) => _dataSource.Subscribe(observer);
    }
}