using Code.Abstractions.Command;
using UnityEngine;
using Zenject;

namespace Code.Core.Buildings
{
    public class MainBuildingCommandQueue:MonoBehaviour, ICommandQueue
    {
        public ICommand CurrentCommand => default;
        [Inject] private CommandExecutorBase<IProduceUnitCommand> _produceCommand;
        [Inject] private CommandExecutorBase<IProduceTarget> _produceTarget;
        public async void EnqueueCommand(object command)
        {
            await _produceCommand.TryExecuteCommand(command);
            await _produceTarget.TryExecuteCommand(command);
        }

        public void Clear()
        {
            
        }
    }
}