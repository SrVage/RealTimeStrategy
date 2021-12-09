using System;
using UniRx;
using UnityEngine;

namespace Code.Core.Unit
{
    public class UnitCollision:MonoBehaviour, IObservable<GameObject>
    {
        public ReactiveProperty<GameObject> CollisionGameObject { get; private set; } =
            new ReactiveProperty<GameObject>();
        private void OnCollisionStay(Collision collisionInfo)
        {
            CollisionGameObject.Value = collisionInfo.gameObject;
        }

        public IDisposable Subscribe(IObserver<GameObject> observer) => 
            CollisionGameObject.Subscribe(observer);
    }
}