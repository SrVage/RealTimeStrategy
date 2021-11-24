using System;
using UnityEngine;

namespace Code.ControlSystem.Scriptable
{
    public class BaseScriptableValue<T>:ScriptableObject
    {
        public T CurrentValue { get; private set; }
        public event Action<T> OnSelected;

        public void SetValue(T selectable)
        {
            CurrentValue = selectable;
            OnSelected?.Invoke(selectable);
        }
    }
}