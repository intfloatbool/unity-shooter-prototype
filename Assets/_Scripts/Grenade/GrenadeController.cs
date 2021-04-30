using System;
using System.Collections.Generic;
using _Scripts.Battle;
using _Scripts.Static;
using UnityEngine;

namespace _Scripts.Grenade
{
    public class GrenadeController : MonoBehaviour, IOwnerable
    {
        [SerializeField] private Grenade _grenadePrefab;
        [SerializeField] private Transform _grenadeLogicRoot;
        [SerializeField] private Transform _grenadeThrowPoint;
        [SerializeField] private GrenadeTrajectory _grenadeTrajectory;

        private bool _isModeActivated;

        private LinkedList<Grenade> _grenadePool = new LinkedList<Grenade>();
        
        public Action OnGrenadeLaunchedCallback { get; set; }
        
        public BattleUnit Owner { get; private set; }
        public void InitOwner(BattleUnit owner)
        {
            this.Owner = owner;
        }

        private void Awake()
        {
            GameHelper.CheckForNull(_grenadeLogicRoot, this);
            GameHelper.CheckForNull(_grenadeTrajectory, this);
            GameHelper.CheckForNull(_grenadeThrowPoint, this);
        }

        public void SetActiveGrenadeMode(bool isActive)
        {
            _grenadeLogicRoot.gameObject.SetActive(isActive);
            var playerCamera = Owner.PlayerCamera;
            GameHelper.CheckForNull(playerCamera, this);
            if (playerCamera != null)
            {
                playerCamera.ThirdPersonCamera.lockCamera = isActive;
            }

            _isModeActivated = isActive;
        }

        private void Update()
        {
            if (_isModeActivated)
            {
                HandleGrenadeLaunchLoop();
            }
        }

        private void HandleGrenadeLaunchLoop()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ThrowGrenade();
            }
        }

        private void ThrowGrenade()
        {
            var grenade = GetGrenadeFromPool();
            grenade.ResetGrenade();
            grenade.Activate();
            grenade.transform.position = _grenadeThrowPoint.position;
            grenade.transform.rotation = _grenadeThrowPoint.rotation;
            var calculatedVelocity = _grenadeTrajectory.CalculateVelocityForGrenade();
            calculatedVelocity = Vector3.ClampMagnitude(calculatedVelocity, 100f);
            grenade.Rb.velocity = calculatedVelocity;
            OnGrenadeLaunchedCallback?.Invoke();   
        }

        private Grenade GetGrenadeFromPool()
        {
            Grenade grenadInstance = default;
            foreach (var pooledGrenade in _grenadePool)
            {
                if (pooledGrenade != null && !pooledGrenade.gameObject.activeInHierarchy)
                {
                    grenadInstance = pooledGrenade;
                    grenadInstance.ResetGrenade();
                    grenadInstance.gameObject.SetActive(true);
                    break;
                }
            }

            if (grenadInstance == null)
            {
                grenadInstance = Instantiate(_grenadePrefab);
                _grenadePool.AddLast(grenadInstance);
            }

            return grenadInstance;
        }
    }
}
