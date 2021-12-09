using System;
using Code.Abstractions;
using Code.Abstractions.Command;
using Zenject;
using UniRx;
using UnityEngine;

namespace Code.UI.UIModel
{
    public class QueueProduceModel
    {
        public IObservable<IUnitProducer> UnitProducers { get; private set; }

        [Inject]
        public void Init(IObservable<ISelectable> currentlySelected)
        {
            UnitProducers = currentlySelected.Select(selectable => selectable as Component)
                .Select(component => component?.GetComponent<IUnitProducer>());
        }
    }
}