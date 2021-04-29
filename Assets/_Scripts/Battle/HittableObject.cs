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
        [SerializeField] private TeamController _teamController;
        
        [Space]
        [SerializeField] private UnityEvent _onDeadUnityEv;
        
        [Space]
        [Header("Runtime")]
        [SerializeField] private AliveData _currentAliveData;


        public float DamageTakingMultipler { get; set; } = 1f;
        
        public event Action<AliveData, HitData> OnDamaged;
        public event Action OnDied; 
        
        private void Awake()
        {
            Assert.IsNotNull(_hittableParams, "_hittableParams != null");
            Assert.IsNotNull(_teamController, "_teamController != null");

            _currentAliveData = new AliveData(
                _hittableParams.MaxHp,
                _hittableParams.MaxHp,
                false
            );
        }

        public void Kill()
        {
            if (_currentAliveData.IsDead)
                return;
            
            _currentAliveData = new AliveData(
                _hittableParams.MaxHp,
                0,
                true
            );
            
            _onDeadUnityEv?.Invoke();
            OnDied?.Invoke();
        }

        public void DealDamage(HitData hitData)
        {
            if (_currentAliveData.IsDead)
                return;

            // check friendly-fire
            if (hitData.unitSource != null && hitData.unitSource.TeamController != null)
            {
                if (hitData.unitSource.TeamController.IsTeammate(_teamController))
                    return;
            }
            
            var lastHp = _currentAliveData.CurrentHp;
            var damage = hitData.damage *DamageTakingMultipler;

            var currentHp = lastHp - damage;
            currentHp = Mathf.Clamp(currentHp, 0, _hittableParams.MaxHp);
            bool isDead = currentHp <= 0;
            _currentAliveData = new AliveData(
                _hittableParams.MaxHp,
                Mathf.RoundToInt(currentHp),
                isDead
            );
            
            OnDamaged?.Invoke(_currentAliveData, hitData);
            
            if (isDead)
            {
                _onDeadUnityEv?.Invoke();
                OnDied?.Invoke();
            }
        }

        public void SelfDamage(int dmg)
        {
            DealDamage(new HitData(
                null, 
                null,
                dmg));
        }
        
    }
}
