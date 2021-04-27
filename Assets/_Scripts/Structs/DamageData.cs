using UnityEngine;

namespace _Scripts.Structs
{
    [System.Serializable]
    public struct DamageData
    {
        [SerializeField] private int _minDamage;
        [SerializeField] private int _maxDamage;

        public int CalculateRandomDamage() => Random.Range(_minDamage, _maxDamage);
    }
}