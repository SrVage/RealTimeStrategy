using Code.Abstractions;
using UnityEngine;

namespace Code.Core
{
    public class CanAttacked:MonoBehaviour, ICanAttacked
    {
        public Transform Transform => transform;
    }
}