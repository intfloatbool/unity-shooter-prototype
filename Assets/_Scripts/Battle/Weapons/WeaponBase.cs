using System;
using _Scripts.Settings;
using UnityEngine;

namespace _Scripts.Battle.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected WeaponParams _weaponParams;
        public WeaponParams weaponParams => _weaponParams;
        
        protected HitData? _lastHitData;
        
        /// <summary>
        /// WeaponBase arg = weapon that being shot
        /// int arg = remains ammo
        /// </summary>
        public virtual event Action<WeaponBase, int> OnShot;

        public void UpdateHitData(HitData hitData)
        {
            _lastHitData = hitData;
        }

        public virtual void Shot()
        {
            if (!_lastHitData.HasValue)
            {
                Debug.LogError($"{nameof(HitData)} is not setup!");
                return;
            }
            
            OnShotStart();
        }

        protected abstract void OnShotStart();
    }
}
