using System;
using Code.Abstractions;
using UnityEngine;

namespace Code.ControlSystem.Scriptable
{
    [CreateAssetMenu (order = 0, menuName = "Config/Selectable")]
    public class SelectableValue:BaseStatefulScriptableValue<ISelectable>
    {
    }
}