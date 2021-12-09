using System;
using Code.UI.UIModel;
using Code.UI.UIView;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.UI.UIPresenter
{
    public class QueueProducePresenter:MonoBehaviour
    {
        [SerializeField] private GameObject _uiHolder;
        private IDisposable _productionQueueAdd;
        private IDisposable _productionQueueRemove;
        private IDisposable _productionQueueReplace;
        private IDisposable _cancelButton;

        [Inject]
        private void Init(QueueProduceView view, QueueProduceModel model)
        {
            model.UnitProducers.Subscribe(unitProducer =>
            {
                _productionQueueAdd?.Dispose();
                _productionQueueRemove?.Dispose();
                _productionQueueReplace?.Dispose();
                _cancelButton?.Dispose();
                view.Clear();
                _uiHolder.SetActive(unitProducer!=null);

                if (unitProducer != null)
                {
                    _productionQueueAdd = unitProducer.Queue.ObserveAdd()
                        .Subscribe(addEvent => view.SetTask(addEvent.Value, addEvent.Index));
                    _productionQueueRemove = unitProducer.Queue.ObserveRemove()
                        .Subscribe(removeEvent => view.SetTask(null, removeEvent.Index));
                    _productionQueueReplace = unitProducer.Queue.ObserveReplace()
                        .Subscribe(replaceEvent => view.SetTask(replaceEvent.NewValue, replaceEvent.Index));
                    _cancelButton = view.CancelButtonClick.Subscribe(unitProducer.Cancel);
                    for (int i = 0; i < unitProducer.Queue.Count; i++)
                    {
                        view.SetTask(unitProducer.Queue[i], i);
                    }
                }
            });
        }
    }
}