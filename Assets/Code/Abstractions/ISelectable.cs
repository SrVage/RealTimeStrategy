using UnityEngine;

namespace Code.Abstractions
{
    public interface ISelectable:IIconHolder, IHealthHolder
    {
        Transform Object { get; }
        void Selecting();
        void Unselecting();
    }
}