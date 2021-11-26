using System;
using System.Linq;
using Code.Abstractions;
using Code.ControlSystem.Scriptable;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Code.ControlSystem
{
    public class MouseInput : MonoBehaviour
    {
        private const string LayerNamesGround = "Ground";
        private const string LayerNamesUnit = "Unit";
        private Camera _camera;
        [Inject] private SelectableValue _selectedbject;
        [Inject] private GroundPointValue _groundPoint;
        [Inject] private AttackedValue _attackedValue;
        [SerializeField] private EventSystem _eventSystem;
        private ISelectable _currentSelect;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
                return;
            if (_eventSystem.IsPointerOverGameObject())
                return;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                var hits = Physics.RaycastAll(ray);
                if (hits.Length == 0)
                    return;
                if (_currentSelect != null)
                    _currentSelect.Unselecting();
                _currentSelect = hits
                    .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                    .FirstOrDefault(c => c != null);
                _selectedbject.SetValue(_currentSelect);
                if (_currentSelect != null)
                    _currentSelect.Selecting();
            }
            else
            {
                LayerMask mask = LayerMask.GetMask(LayerNamesGround)|LayerMask.GetMask(LayerNamesUnit);
                if (Physics.Raycast(ray, out var hit, mask))
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
    }
}