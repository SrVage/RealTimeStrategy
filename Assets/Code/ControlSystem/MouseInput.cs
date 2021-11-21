using System;
using System.Linq;
using Code.Abstractions;
using UnityEngine;

namespace Code.ControlSystem
{
    public class MouseInput:MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private SelectableValue _selectedbject;
        private ISelectable _currentSelect;
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;
            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            if (hits.Length ==0) 
                return;
            if (_currentSelect!=null)
                _currentSelect.Unselecting();
            _currentSelect = hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .FirstOrDefault(c => c != null);
            _selectedbject.SetValue(_currentSelect);
            if (_currentSelect!=null)
                _currentSelect.Selecting();
            /*foreach (var hit in hits)
            {
                var selected = hit.collider.GetComponentInParent<ISelectable>();
                /*if (selected!=null)
                   selected.Selecting();#1#
                if(selected==null)
                    continue;
                _selectedbject.SetValue(selected);
            }*/
        }
    }
}