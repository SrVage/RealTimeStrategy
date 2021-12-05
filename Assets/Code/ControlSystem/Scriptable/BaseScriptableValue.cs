using System;
using Code.Abstractions;
using Code.Abstractions.Command;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Code.ControlSystem.Scriptable
{
    public abstract class BaseScriptableValue<T>:ScriptableObject, IAwaitable<T>
    {
        public class NewValueNotifier<TAwaited>:AwaiterBase<TAwaited>
        {
            private readonly BaseScriptableValue<TAwaited> _baseScriptableValue;
            private TAwaited _result;

            public override TAwaited GetResult() => _result;

            public NewValueNotifier(BaseScriptableValue<TAwaited> baseScriptableValue)
            {
                _baseScriptableValue = baseScriptableValue;
                _baseScriptableValue.OnSelected += Selected;
            }           
            public NewValueNotifier(BaseScriptableValue<TAwaited> baseScriptableValue, IObservable<TAwaited> observable)
            {
                _baseScriptableValue = baseScriptableValue;
                observable.Subscribe(Selected);
            }

            private void Selected(TAwaited obj)
            {
                _baseScriptableValue.OnSelected -= Selected;
                _result = obj;
                _isCompleted = true;
                StartEvent();
            }
        }
        public T CurrentValue { get; private set; }
        public virtual event Action<T> OnSelected;

        public virtual void SetValue(T selectable)
        {
            CurrentValue = selectable;
            OnSelected?.Invoke(selectable);
        }
        
        public virtual IAwaiter<T> GetAwaiter() => 
            new NewValueNotifier<T>(this);
    }
}