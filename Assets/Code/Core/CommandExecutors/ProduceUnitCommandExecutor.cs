using System.Threading.Tasks;
using Code.Abstractions.Command;
using Code.ControlSystem.Command;
using Code.Core.Buildings;
using Code.Core.Unit;
using Code.Tools;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Core.CommandExecutors
{
    public class ProduceUnitCommandExecutor:CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;
        [SerializeField] private int MaxUnitInQueue = 6;
        [SerializeField] private Transform _unitTransform;
        [Inject] private DiContainer _container;
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
                unit.GetComponent<FractionMember>().SetFraction(gameObject.GetComponent<FractionMember>().FractionID);
                _count++;
            }
        }

        public void Cancel(int index) => _queue.RemoveAtIndex(index);
        public override async Task ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            Debug.Log("produce");
            _queue.Add(new UnitProductionTask(command.Icon, command.Name, command.ProductionTime, command.UnitPrefab));
            /*var instance = _container.InstantiatePrefab(command.UnitPrefab, transform.position, Quaternion.identity, _unitTransform);
            var queue = instance.GetComponent<ICommandQueue>();
            var fractionMember = instance.GetComponent<FractionMember>();
            fractionMember.SetFraction(GetComponent<FractionMember>().FractionID);*/
            //queue.EnqueueCommand(new MoveCommand(_spawnPoint));
        }
    }
}