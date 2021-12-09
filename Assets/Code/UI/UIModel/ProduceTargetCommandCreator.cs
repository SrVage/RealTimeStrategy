using Code.Abstractions.Command;
using Code.ControlSystem.Command;
using UnityEngine;

namespace Code.UI.UIModel
{
    public class ProduceTargetCommandCreator:CancelableCommandCreatorBase<IProduceTarget, Vector3>
    {
        protected override IProduceTarget CreateCommand(Vector3 argument) => 
            new ProduceTargetCommand(argument);
    }
}