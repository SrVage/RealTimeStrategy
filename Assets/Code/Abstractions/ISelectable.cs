using UnityEngine;

namespace Code.Abstractions
{
    public interface ISelectable
    {
        string Name { get; }
        float Health { get; }
        float MaxHealth { get; }
        Sprite Icon { get; }
        void Selecting();
        void Unselecting();
    }
}