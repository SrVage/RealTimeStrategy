using UnityEngine;

namespace Code.Abstractions
{
    public interface ISelectable:IIconHolder, IHealthHolder
    {
        void Selecting();
        void Unselecting();
    }
}