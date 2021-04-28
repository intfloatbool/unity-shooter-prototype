using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Settings;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Battle.Weapons
{
    public class FirearmWeapon : WeaponBase
    {
        [SerializeField] private WeaponParams _weaponParams;
        public WeaponParams weaponParams => _weaponParams;

        [SerializeField] private Transform _muzzleAnchor;

        [Space]
        [Header("Runtime")]
        [SerializeField] private int _currentMagazine;
        public int currentMagazine => _currentMagazine;
        
        [SerializeField] private bool _isReadyToShot;
        [SerializeField] private bool _isReloadRequired;
        [SerializeField] private bool _isOnReloadProcess;
        
        private float _reloadTimer;
        private float _shotDelayTimer;

        private LinkedList<GameObject> _projectilesPool;

        /// <summary>
        /// int arg = remains ammo
        /// </summary>
        public event Action<int> OnShot;
        public event Action OnReloadStart;
        public event Action OnReloadDone;

        private void Awake()
        {
            Assert.IsNotNull(_weaponParams, "_weaponParams != null");

            if (_muzzleAnchor == null)
            {
                _muzzleAnchor = transform;
            }
            
            SetupWeaponByParams();
        }
        
        private void SetupWeaponByParams()
        {
            _projectilesPool = new LinkedList<GameObject>();
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
            
            HandleShot();
            
            OnShot?.Invoke(_currentMagazine);
        }

        private void HandleShot()
        {
            ShowProjectiles();
            DetectHittable();
        }

        private void ShowProjectiles()
        {
            var projectilePrefab = _weaponParams.ProjectilePrefab;
            if (projectilePrefab == null)
                return;

            Vector3 posToSpawn = _muzzleAnchor.transform.position;
            Vector3 currentOffset = _weaponParams.projectilesOffset;
            for (int i = 0; i < _weaponParams.projectilesPerShot; i++)
            {
                var projectile = GetProjectile(projectilePrefab);
                projectile.transform.position = posToSpawn;
                projectile.transform.rotation = _muzzleAnchor.transform.rotation;

                posToSpawn += (currentOffset * (i + 1));
            }
        }

        private void DetectHittable()
        {
            for (int i = 0; i < _weaponParams.projectilesPerShot; i++)
            {
                if (Physics.Raycast(_muzzleAnchor.transform.position,
                    _muzzleAnchor.forward, out RaycastHit hit, Mathf.Infinity))
                {
                    if (hit.collider.TryGetComponent(out HittableObject hittableObject))
                    {
                        if (_lastHitData != null)
                            hittableObject.DealDamage(
                                _lastHitData.Value
                            );
                    }
                }
            }
            
        }

        private GameObject GetProjectile(GameObject prefab)
        {
            var freeProjectile = _projectilesPool.FirstOrDefault(p => p != null && !p.activeInHierarchy);
            if (freeProjectile == null)
            {
                freeProjectile = Instantiate(prefab);
                _projectilesPool.AddLast(freeProjectile);
            }
            else
            {
                freeProjectile.gameObject.SetActive(true);
            }

            return freeProjectile;
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
            _isReadyToShot = true;
            OnReloadDone?.Invoke();
        }

        public void ReloadWeapon()
        {
            _isReloadRequired = true;
            _isOnReloadProcess = true;
            _isReadyToShot = false;
            OnReloadStart?.Invoke();
        }
    }
}
