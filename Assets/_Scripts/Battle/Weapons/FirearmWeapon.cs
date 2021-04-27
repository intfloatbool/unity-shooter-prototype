using System;
using _Scripts.Settings;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Battle.Weapons
{
    public class FirearmWeapon : WeaponBase
    {
        [SerializeField] private WeaponParams _weaponParams;
        
        [Space]
        [Header("Runtime")]
        [SerializeField] private bool _isReadyToShot;


        private float _reloadTimer;
        private float _shotDelayTimer;
        
        private void Awake()
        {
            Assert.IsNotNull(_weaponParams, "_weaponParams != null");
        }

        protected override void OnShotStart()
        {
            if (!_isReadyToShot)
                return;
            
            Debug.Log($"{name} shot!");
            _isReadyToShot = false;
        }

        private void Update()
        {
            HandleWeaponLoop();
        }

        private void HandleWeaponLoop()
        {
            HandleShotDelayTimerLoop();
        }

        private void HandleShotDelayTimerLoop()
        {
            if (_isReadyToShot)
                return;

            if (_shotDelayTimer >= _weaponParams.delay)
            {
                _isReadyToShot = true;
                _shotDelayTimer = 0f;
                return;
            }

            _shotDelayTimer += Time.deltaTime;
        }
    }
}
