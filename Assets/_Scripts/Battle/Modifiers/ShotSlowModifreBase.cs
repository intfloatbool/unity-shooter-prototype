using System.Collections.Generic;
using _Scripts.Battle.Weapons;
using _Scripts.Settings;
using _Scripts.Static;
using UnityEngine;

namespace _Scripts.Battle.Modifiers
{
    public abstract class ShotSlowModifreBase : MonoBehaviour
    {
        [SerializeField] protected SlowModiferSettings _slowModiferSettings;
        [SerializeField] protected float _slowTime = 0.6f;
        [SerializeField] protected BattleUnit _battleUnit;
        
        private IReadOnlyCollection<WeaponBase> _weapons;
        
        private void OnValidate()
        {
            if (_battleUnit == null)
            {
                _battleUnit = GetComponentInParent<BattleUnit>();
            }
        }

        private void Awake()
        {
            GameHelper.CheckForNull(_battleUnit, this);
            GameHelper.CheckForNull(_battleUnit.WeaponController, this);
            GameHelper.CheckForNull(_slowModiferSettings, this);

            _weapons = _battleUnit.WeaponController.PossibleWeapons;
            foreach (var weapon in _weapons)
            {
                weapon.OnShot += WeaponOnShot;
            }
        }

        private void WeaponOnShot(WeaponBase weapon, int remainsAmmo)
        {
            
        }

        protected abstract void OnModiferStart();
        protected abstract void OnModiferStop();
    }
}