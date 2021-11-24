using Code.Abstractions;
using Code.Tools;
using UnityEngine;

namespace Code.Core
{
    public class SelectingView:MonoBehaviour, ISelectable
    {
        public string Name => gameObject.name;
        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;
        
        [SerializeField] private float _health;
        [SerializeField] private Sprite _icon;
        private float _maxHealth = 1000;
        public void Selecting()
        {
            GetComponent<Outline>().enabled = true;
        }

        public void Unselecting()
        {
            GetComponent<Outline>().enabled = false;
        }
    }
}