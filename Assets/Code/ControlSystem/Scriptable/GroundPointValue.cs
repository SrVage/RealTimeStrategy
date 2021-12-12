using System;
using UnityEngine;

namespace Code.ControlSystem.Scriptable
{
    [CreateAssetMenu (order = 1, menuName = "Config/Ground")]
    public class GroundPointValue:BaseStatelessScriptableValue<Vector3>
    {
    }
}