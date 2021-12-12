using UnityEngine;

namespace Code.Core.Buildings
{
    public class MainBuilding:MonoBehaviour
    {
        [SerializeField] private Transform _unitParent;
        [SerializeField] private int _productionTime=2000;
        private float _maxHealth=1000;
        private int _count = 0;
    }
}