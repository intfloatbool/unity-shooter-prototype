using UnityEngine;

namespace _Scripts.Structs
{
    [System.Serializable]
    public struct AliveData
    {
        [SerializeField] private int _currentHp;
        public int CurrentHp => _currentHp;
        
        [SerializeField] private bool _isDead;
        public bool IsDead => _isDead;

        public AliveData(int currentHpt, bool isDead)
        {
            this._currentHp = currentHpt;
            this._isDead = isDead;
        }
    }
}