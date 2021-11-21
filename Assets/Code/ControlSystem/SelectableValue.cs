using System;
using Code.Abstractions;
using UnityEngine;

namespace Code.ControlSystem
{
    [CreateAssetMenu (order = 0, menuName = "Config/Selectable")]
    public class SelectableValue:ScriptableObject
    {
        public ISelectable CurrentValue { get; private set; }
        public event Action<ISelectable> OnSelected;

        public void SetValue(ISelectable selectable)
        {
            CurrentValue = selectable;
            OnSelected?.Invoke(selectable);
        }
    }
}