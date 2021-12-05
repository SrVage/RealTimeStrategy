using Code.Abstractions;
using Code.ControlSystem;
using Code.ControlSystem.Scriptable;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.UIView
{
    public class ObjectInfoUI:MonoBehaviour
    {
        [SerializeField] private Image _objectAvatar;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _objectName;
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private Image _healthSliderBackground;
        [SerializeField] private Image _healthSliderFillImage;
        [Inject] private SelectableValue _selectable;

        private void Start()
        {
            _selectable.Result.Subscribe(OnSelected);
        }

        private void OnSelected(ISelectable selected)
        {
            SetVisible(selected);
            if (selected==null) return;
            _objectAvatar.sprite = selected.Icon;
            _objectName.text = selected.Name;
            _health.text = $"{selected.Health}/{selected.MaxHealth}";
            _healthSlider.minValue = 0;
            _healthSlider.maxValue = selected.MaxHealth;
            _healthSlider.value = selected.Health;
            Color color = Color.Lerp(Color.red, Color.green, selected.Health / selected.MaxHealth);
            _healthSliderBackground.color = color * 0.5f;
            _healthSliderFillImage.color = color;
        }

        private void SetVisible(ISelectable selected)
        {
            bool visible = selected != null;
            _objectAvatar.enabled = visible;
            _healthSlider.gameObject.SetActive(visible);
            _objectName.enabled = visible;
            _health.enabled = visible;
        }
    }
}