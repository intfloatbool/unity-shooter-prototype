using System;
using _Scripts.Settings;
using _Scripts.Structs;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace _Scripts.Battle
{
    public class HittableObject : MonoBehaviour
    {
        [SerializeField] private HittableParams _hittableParams;

        [Space]
        [SerializeField] private UnityEvent _onDeadUnityEv;
        
        [Space]
        [Header("Runtime")]
        [SerializeField] private AliveData _currentAliveData;

        public event Action<AliveData, HitData> OnDamaged;
        public event Action OnDied; 
        
        private void Awake()
        {
            Assert.IsNotNull(_hittableParams, "_hittableParams != null");

            _currentAliveData = new AliveData(
                _hittableParams.MaxHp,
                false
            );
        }

        public void DealDamage(HitData hitData)
        {
            if (_currentAliveData.IsDead)
                return;

            var lastHp = _currentAliveData.CurrentHp;
            var damage = hitData.damage;

            var currentHp = lastHp - damage;
            currentHp = Mathf.Clamp(currentHp, 0, _hittableParams.MaxHp);
            bool isDead = currentHp <= 0;
            _currentAliveData = new AliveData(
                currentHp,
                isDead
            );
            
            if (isDead)
            {
                _onDeadUnityEv?.Invoke();
                OnDied?.Invoke();
            }
            
            Debug.Log($"{name} damaged of {hitData.damage} by {hitData.unitSource.name} with weapon {hitData.weaponSource.name}!");
        }
        
    }
}
