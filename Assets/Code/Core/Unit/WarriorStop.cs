using System;
using System.Threading;
using System.Threading.Tasks;
using Code.Abstractions.Command;
using UniRx;
using UnityEngine;

namespace Code.Core.Unit
{
    public class WarriorStop:CommandExecutorBase<IStopCommand>
    {
        private const int CollisionTime = 200;
        public CancellationTokenSource CancellationTokenSource { get; set; }
        private GameObject _collisionObject = null;
        private bool _collision = false;
        private int count=0;

        private void Awake()
        {
            gameObject.GetComponent<UnitCollision>().Subscribe(CollisionDetect);
        }

        private void CollisionDetect(GameObject obj)
        {
            if (_collisionObject == false)
            {
                _collisionObject = obj;
                _collision = true;
                count = 0;
            }

            if (obj == _collisionObject)
            {
                count++;
                if (count > CollisionTime)
                {
                    CancellationTokenSource?.Cancel();
                }
            }
        }

        public override async Task ExecuteSpecificCommand(IStopCommand command)
        {
            CancellationTokenSource?.Cancel();
        }
    }
}