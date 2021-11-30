using System;
using Code.Abstractions;
using Code.Abstractions.Command;
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

            private void Selected(TAwaited obj)
            {
                _baseScriptableValue.OnSelected -= Selected;
                _result = obj;
                _isCompleted = true;
                StartEvent();
            }
        }
        public T CurrentValue { get; private set; }
        public event Action<T> OnSelected;

        public void SetValue(T selectable)
        {
            CurrentValue = selectable;
            OnSelected?.Invoke(selectable);
        }

        public IAwaiter<T> GetAwaiter() => 
            new NewValueNotifier<T>(this);
    }
}