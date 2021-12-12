using System;
using Code.Abstractions;
using Code.Abstractions.Command;
using UnityEngine;

namespace Code.ControlSystem.Scriptable
{
    public abstract class BaseScriptableValue<T>:ScriptableObject, IAwaitable<T>
    {
        public class NewValueNotifier<TAwaited>:AwaiterBase<TAwaited>
        {
            private readonly BaseScriptableValue<TAwaited> _baseScriptableValue;

            public NewValueNotifier(BaseScriptableValue<TAwaited> baseScriptableValue)
            {
                _baseScriptableValue = baseScriptableValue;
                _baseScriptableValue.OnSelected += Selected;
            }

            private void Selected(TAwaited obj)
            {
                _baseScriptableValue.OnSelected -= Selected;
                ONWaitFinish(obj);
            }
        }
        public T CurrentValue { get; private set; }
        public event Action<T> OnSelected;

        public virtual void SetValue(T selectable)
        {
            CurrentValue = selectable;
            OnSelected?.Invoke(selectable);
        }
        
        public virtual IAwaiter<T> GetAwaiter()
        {
            return new NewValueNotifier<T>(this);
        }
    }
}