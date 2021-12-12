using Code.Abstractions.Command;
using Code.ControlSystem.Command;
using UnityEngine;

namespace Code.UI.UIModel
{
    public class MoveCommandCreator:CancelableCommandCreatorBase<IMoveCommand, Vector3>
    {
        protected override IMoveCommand CreateCommand(Vector3 argument)
        {
            return new MoveCommand(argument);
        }
    }
}