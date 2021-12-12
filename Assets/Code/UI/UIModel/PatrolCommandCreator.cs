using System;
using Code.Abstractions.Command;
using Code.ControlSystem.Command;
using Code.ControlSystem.Scriptable;
using UnityEngine;
using Zenject;

namespace Code.UI.UIModel
{
    public class PatrolCommandCreator:CancelableCommandCreatorBase<IPatrolCommand, Vector3>
    {
        [Inject] private SelectableValue _selectableValue;
        protected override IPatrolCommand CreateCommand(Vector3 argument) => 
            new PatrolCommand(argument, _selectableValue.Result.Value.Object.position);
        
    }
}