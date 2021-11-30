using System;
using Code.Abstractions.Command;
using Code.ControlSystem.Command;
using UnityEngine;

namespace Code.UI.UIModel
{
    public class PatrolCommandCreator:CancelableCommandCreatorBase<IPatrolCommand, Vector3>
    {
        protected override IPatrolCommand CreateCommand(Vector3 argument) => 
            new PatrolCommand(argument);
    }
}