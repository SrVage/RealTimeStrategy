using System;
using Code.Abstractions.Command;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.UIView
{
    public class QueueProduceView:MonoBehaviour
    {
        public IObservable<int> CancelButtonClick => _cancelButtonClick;
        [SerializeField] private Slider _productionTimeLeft;
        [SerializeField] private TextMeshProUGUI _productionUnitName;
        [SerializeField] private Image[] _images;
        [SerializeField] private GameObject[] _imageHolders;
        [SerializeField] private Button[] _buttons;
        
        private Subject<int> _cancelButtonClick = new Subject<int>();
        private IDisposable _unitProductionTask;

        [Inject]
        private void Init()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                var index = i;
                _buttons[i].OnClickAsObservable().Subscribe(_=>_cancelButtonClick.OnNext(index));
            }
        }

        public void SetTask(IUnitProductionTask task, int index)
        {
            if (task == null)
            {
                _imageHolders[index].SetActive(false);
                _images[index].sprite = null;
                if (index == 0) 
                    ClearCurrent();
                
            }
            else
            {
                _imageHolders[index].SetActive(true);
                _images[index].sprite = task.Icon;
                if (index == 0)
                {
                    _productionUnitName.text = task.Name;
                    _productionUnitName.enabled = true;
                    _productionTimeLeft.gameObject.SetActive(true);
                    _unitProductionTask = Observable.EveryUpdate()
                        .Subscribe(_ => _productionTimeLeft.value = task.TimeLeft / task.ProductionTime);
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _images.Length; i++)
            {
                _images[i].sprite = null;
                _imageHolders[i].SetActive(false);
            }
            ClearCurrent();
        }

        private void ClearCurrent()
        {
            _productionUnitName.text = String.Empty;
            _productionUnitName.enabled = false;
            _productionTimeLeft.gameObject.SetActive(false);
            _unitProductionTask?.Dispose();
        }
    }
}