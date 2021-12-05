using System;
using Code.Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Core
{
    public class TimeModel:ITickable, ITimeModel
    {
        public IObservable<int> GameTime => _gameTime.Select(f => (int) f);
        private ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();
        public void Tick()
        {
            _gameTime.Value += Time.deltaTime;
        }

    }
}