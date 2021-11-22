using Code.Abstractions;
using Code.Abstractions.Command;
using Code.Tools;
using UnityEngine;

namespace Code.Core
{
    public class SecondBuilding:CommandExecutorBase<IProduceUnitCommand>,ISelectable
    {
        public string Name => gameObject.name;
        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;
        [SerializeField] private float _health;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _productionPrefab;
        [SerializeField] private Transform _unitParent;
        private float _maxHealth=1000;
        private int _count = 0;

        public void Selecting()
        {
            GetComponent<Outline>().enabled = true;
        }

        public void Unselecting()
        {
            GetComponent<Outline>().enabled = false;
        }
        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            Instantiate(command.UnitPrefab, transform.position, Quaternion.identity);
        }
    }
}