using System.Threading.Tasks;
using Code.Abstractions.Command;
using UnityEngine;

namespace Code.Core.CommandExecutors
{
    public class ProduceTargetCommandExecutor:CommandExecutorBase<IProduceTarget>
    {
        [SerializeField] private ProduceUnitCommandExecutor _produceUnitCommandExecutor;
        public override async Task ExecuteSpecificCommand(IProduceTarget command)
        {
            Debug.Log(command.Target);
            _produceUnitCommandExecutor.SetSpawnPoint(command.Target);
        }
    }
}