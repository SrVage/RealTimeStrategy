using UnityEngine;

namespace Code.Abstractions
{
    public interface ICanAttacked:IHealthHolder
    {
        Transform Transform { get; }
        void ReceivedDamage(float amount);
    }
}