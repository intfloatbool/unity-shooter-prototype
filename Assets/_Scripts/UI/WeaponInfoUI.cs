using System;
using _Scripts.Battle;
using _Scripts.Battle.Weapons;
using _Scripts.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class WeaponInfoUI : MonoBehaviour, IOwnerable
    {
        [SerializeField] private TextMeshProUGUI _ammoText;
        [SerializeField] private Image _reloadImg;
        [SerializeField] private Image _weaponIconImg;
        
        public BattleUnit Owner { get; private set; }

        private BattleUnit _lastOwner;
        private FirearmWeapon _lastWeapon;
        private WeaponParams _currentWeaponParams;

        [Space]
        [Header("Debug")]
        [SerializeField] private BattleUnit _debugUnitOwner;
        
        private void Awake()
        {
            Assert.IsNotNull(_ammoText, "_ammoText != null");
            Assert.IsNotNull(_reloadImg, "_reloadImg != null");
            Assert.IsNotNull(_weaponIconImg, "_weaponIconImg != null");
        }

        private void Start()
        {
            if (_debugUnitOwner != null)
            {
                InitOwner(_debugUnitOwner);
            }
        }

        public void InitOwner(BattleUnit owner)
        {
            if (_lastOwner != null)
            {
                UnsubscribeFromLastOwnerWeapon();
            }
            
            this.Owner = owner;
            SubscribeToNewOwnerWeapon();

            _lastOwner = this.Owner;
        }

        private void SubscribeToNewOwnerWeapon()
        {
            var weaponController = this.Owner.WeaponController;
            if (weaponController == null)
                return;
            
            weaponController.OnWeaponChanged += UpdateUiByWeapon;
            if (weaponController.CurrentWeapon != null)
            {
                UpdateUiByWeapon(weaponController.CurrentWeapon);
            }
            
        }
        

        private void UnsubscribeFromLastOwnerWeapon()
        {
            var weaponController = _lastOwner.WeaponController;
            if (weaponController == null)
                return;
            weaponController.OnWeaponChanged -= UpdateUiByWeapon;
        }
        
        private void UpdateUiByWeapon(WeaponBase weapon)
        {
            var firearmWeapon = weapon as FirearmWeapon;

            if (firearmWeapon == null)
            {
                return;
            }
            
            _currentWeaponParams = firearmWeapon.weaponParams;
            Assert.IsNotNull(_currentWeaponParams, "_currentWeaponParams != null");

            if (_lastWeapon != null)
            {
                _lastWeapon.OnShot -= WeaponOnShot;
                _lastWeapon.OnReloadStart -= WeaponOnReloadStart;
                _lastWeapon.OnReloadDone -= WeaponOnReloadDone;
            }
            
            _lastWeapon = firearmWeapon;
            
            _lastWeapon.OnShot += WeaponOnShot;
            _lastWeapon.OnReloadStart += WeaponOnReloadStart;
            _lastWeapon.OnReloadDone += WeaponOnReloadDone;

            UpdateAmmoText();
            UpdateIcon();

        }

        private void UpdateAmmoText()
        {
            _ammoText.text = $"{_lastWeapon.currentMagazine}/{_currentWeaponParams.magazineAmount}";
        }

        private void UpdateIcon()
        {
            _weaponIconImg.sprite = _currentWeaponParams.spriteIcon;
        }

        private void WeaponOnReloadStart()
        {
            _ammoText.gameObject.SetActive(false);
            _reloadImg.gameObject.SetActive(true);
        }

        private void WeaponOnReloadDone()
        {
            _ammoText.gameObject.SetActive(true);
            _reloadImg.gameObject.SetActive(false);
            
            UpdateAmmoText();
        }

        private void WeaponOnShot(int currentMagazine)
        {
            UpdateAmmoText();
        }
    }
}
