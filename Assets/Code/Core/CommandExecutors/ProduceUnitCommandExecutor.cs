using System;
using Code.Abstractions.Command;
using Code.Core.Buildings;
using Code.Core.Unit;
using Code.Tools;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Core.CommandExecutors
{
    public class ProduceUnitCommandExecutor:CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;
        [SerializeField] private int MaxUnitInQueue = 6;
        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();
        private int _count = 0;
        private Vector3 _spawnPoint;

        private void Awake()
        {
            _spawnPoint = transform.position + new Vector3(2, 0, 0);
        }

        public void SetSpawnPoint(Vector3 spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }

        private void Update()
        {
            if (_queue.Count==0)
                return;
            var innerTask = (UnitProductionTask) _queue[0];
            innerTask.TimeLeft -= Time.deltaTime;
            if (innerTask.TimeLeft <= 0)
            {
                _queue.RemoveAtIndex(0);
                Vector3 offset = _spawnPoint+ new Vector3(_count%5, 0, _count/5);
                var unit = Instantiate(innerTask.UnitPrefab, transform.position,
                    Quaternion.identity);
                unit.GetComponent<WarriorMovementStop>().SetDestination(offset);
                _count++;
            }
        }

        public void Cancel(int index) => _queue.RemoveAtIndex(index);
        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            _queue.Add(new UnitProductionTask(command.Icon, command.Name, command.ProductionTime, command.UnitPrefab));
        }
    }
}