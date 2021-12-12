using Code.Abstractions;
using UnityEngine;

namespace Code.Core
{
    public class FractionMember:MonoBehaviour, IFractionMember
    {
        public Fraction FractionID => _fractionID;
        [SerializeField] private Fraction _fractionID;

        public void SetFraction(Fraction fraction)
        {
            _fractionID = fraction;
        }
    }
}