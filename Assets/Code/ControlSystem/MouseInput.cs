using System;
using System.Linq;
using Code.Abstractions;
using Code.ControlSystem.Scriptable;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Code.ControlSystem
{
    public class MouseInput : MonoBehaviour
    {
        private const string LayerNamesGround = "Ground";
        private const string LayerNamesUnit = "Unit";
        LayerMask mask;
        private Camera _camera;
        [Inject] private SelectableValue _selectedbject;
        [Inject] private GroundPointValue _groundPoint;
        [Inject] private AttackedValue _attackedValue;
        [SerializeField] private EventSystem _eventSystem;
        private ISelectable _currentSelect;

        private void Awake()
        {
            mask = LayerMask.GetMask(LayerNamesGround) | LayerMask.GetMask(LayerNamesUnit);
            _camera = Camera.main;
            var leftClickStream = Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Where(_ => !_eventSystem.IsPointerOverGameObject())
                .Select(hit => Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition)))
                .Where(hits => hits.Length>0);
            var rightClickStream = Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(1))
                .Where(_ => !_eventSystem.IsPointerOverGameObject())
                .Select(h => (Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition), mask)));
            leftClickStream.Subscribe(hit=>LeftMouse(hit));
            rightClickStream.Subscribe(hit => RightMouse(hit.FirstOrDefault()));
        }

        private void LeftMouse(RaycastHit[] hits)
        {
            if (_currentSelect != null)
                _currentSelect.Unselecting();
            _currentSelect = hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .FirstOrDefault(c => c != null);
            _selectedbject.SetValue(_currentSelect);
            if (_currentSelect != null)
                _currentSelect.Selecting();
        }

        private void RightMouse(RaycastHit hit)
        {
            if (hit.collider.GetComponentInParent<ICanAttacked>()!=null)
            {
                _attackedValue.SetValue(hit.collider.GetComponentInParent<ICanAttacked>());
            }
            else
                _groundPoint.SetValue(hit.point);
        }
    }
}