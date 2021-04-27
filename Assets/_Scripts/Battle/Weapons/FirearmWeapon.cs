using System;
using _Scripts.Settings;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Battle.Weapons
{
    public class FirearmWeapon : WeaponBase
    {
        [SerializeField] private WeaponParams _weaponParams;
        public WeaponParams weaponParams => _weaponParams;
        [Space]
        [Header("Runtime")]
        [SerializeField] private int _currentMagazine;

        public int currentMagazine => _currentMagazine;
        
        [SerializeField] private bool _isReadyToShot;
        [SerializeField] private bool _isReloadRequired;
        [SerializeField] private bool _isOnReloadProcess;
        
        private float _reloadTimer;
        private float _shotDelayTimer;

        /// <summary>
        /// int arg = remains ammo
        /// </summary>
        public event Action<int> OnShot;
        public event Action OnReloadStart;
        public event Action OnReloadDone;
        
        private void Awake()
        {
            Assert.IsNotNull(_weaponParams, "_weaponParams != null");
            SetupWeaponByParams();
        }
        
        private void SetupWeaponByParams()
        {
            _currentMagazine = _weaponParams.magazineAmount;
        }

        protected override void OnShotStart()
        {
            if (!_isReadyToShot)
                return;

            if (_isReloadRequired)
                return;
            
            if (_isOnReloadProcess)
                return;

            Debug.Log($"{name} shot!");
            _isReadyToShot = false;

            _currentMagazine -= _weaponParams.projectilesPerShot;
            _currentMagazine = Mathf.Clamp(_currentMagazine, 0, _weaponParams.magazineAmount);

            _isReloadRequired = _currentMagazine <= 0;
            
            OnShot?.Invoke(_currentMagazine);
        }

        private void Update()
        {
            HandleWeaponLoop();
        }

        private void HandleWeaponLoop()
        {
            HandleShotDelayTimerLoop();
            HandleReloadProcessLoop();
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

        private void HandleReloadProcessLoop()
        {
            if (!_isReloadRequired)
                return;

            if (!_isOnReloadProcess)
                return;

            if (_reloadTimer >= _weaponParams.reloadTime)
            {
                _reloadTimer = 0f;
                DoneReload();
                return;
            }

            _reloadTimer += Time.deltaTime;
        }

        private void DoneReload()
        {
            _currentMagazine = _weaponParams.magazineAmount;
            _isReloadRequired = false;
            _isOnReloadProcess = false;
            OnReloadDone?.Invoke();
        }

        public void ReloadWeapon()
        {
            _isOnReloadProcess = true;
            OnReloadStart?.Invoke();
        }
    }
}
