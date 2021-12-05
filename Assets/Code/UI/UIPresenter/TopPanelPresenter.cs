using System;
using Code.Abstractions;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.UIPresenter
{
    public class TopPanelPresenter:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private Button _menuButton;
        [SerializeField] private GameObject _menuPanel;

        [Inject]
        private void Init(ITimeModel timeModel)
        {
            timeModel.GameTime.Subscribe(seconds =>
            {
                var t = TimeSpan.FromSeconds(seconds);
                _timeText.text = String.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            });
            _menuButton.OnClickAsObservable().Subscribe(_=> _menuPanel.SetActive(true));
        }
    }
}