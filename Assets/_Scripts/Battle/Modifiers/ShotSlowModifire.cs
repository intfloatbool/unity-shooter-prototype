using System.Collections.Generic;
using _Scripts.Battle.Base;
using _Scripts.Battle.Weapons;
using _Scripts.Settings;
using _Scripts.Static;
using UnityEngine;

namespace _Scripts.Battle.Modifiers
{
    public class ShotSlowModifire : MonoBehaviour
    {
        [SerializeField] private ShotSlowModiferSettings shotSlowModiferSettings;
        [SerializeField] private BattleUnit _battleUnit;
        [SerializeField] private UnitSpeedControllerBase _unitSpeedController;
        
        private IReadOnlyCollection<WeaponBase> _weapons;

        private WeaponBase _lastShotWeapon;
        private float _slowPerFrameTimer;
        private bool _isOnShotProcess;
        
        private void OnValidate()
        {
            if (_battleUnit == null)
            {
                _battleUnit = GetComponentInParent<BattleUnit>();
                
                if (_battleUnit == null)
                {
                    _battleUnit = GetComponentInChildren<BattleUnit>();
                }
            }
            
            if (_unitSpeedController == null)
            {
                _unitSpeedController = GetComponentInParent<UnitSpeedControllerBase>();
                
                if (_unitSpeedController == null)
                {
                    _unitSpeedController = GetComponentInChildren<UnitSpeedControllerBase>();
                }
            }
        }

        private void Awake()
        {
            GameHelper.CheckForNull(_battleUnit, this);
            GameHelper.CheckForNull(_battleUnit.WeaponController, this);
            GameHelper.CheckForNull(shotSlowModiferSettings, this);

            _weapons = _battleUnit.WeaponController.PossibleWeapons;
            foreach (var weapon in _weapons)
            {
                weapon.OnShot += WeaponOnShot;
            }
        }

        private void OnDestroy()
        {
            if (_weapons != null)
            {
                foreach (var weapon in _weapons)
                {
                    weapon.OnShot -= WeaponOnShot;
                }
            }
        }
        

        private void WeaponOnShot(WeaponBase weapon, int remainsAmmo)
        {
            _isOnShotProcess = true;
            _slowPerFrameTimer = 0;
            _lastShotWeapon = weapon;
            OnModiferStart();
        }

        private void OnModiferStart()
        {
            GameHelper.CheckForNull(_lastShotWeapon);
            
            var slowMultipler =
                shotSlowModiferSettings.GetSpeedMultiplerByWeaponType(_lastShotWeapon.weaponParams.WeaponType);
            
            _unitSpeedController.SetCurrentSpeedMultipler(slowMultipler);
        }

        private void OnModiferStop()
        {
            _unitSpeedController.ResetSpeedToDefault();
        }

        private void Update()
        {
            if (!_isOnShotProcess)
                return;

            if (_slowPerFrameTimer >= shotSlowModiferSettings.slowTime)
            {
                _slowPerFrameTimer = 0;
                _isOnShotProcess = false;
                OnModiferStop();
            }
            
            _slowPerFrameTimer += Time.deltaTime;
        }
    }
}