using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Battle.Weapons
{
    public abstract class WeaponControllerBase : MonoBehaviour, IOwnerable
    {
        [SerializeField] protected List<WeaponBase> _possibleWeapons;
        public IReadOnlyCollection<WeaponBase> PossibleWeapons => _possibleWeapons;
        
        [Space]
        [Header("Runtime")]
        [SerializeField] protected WeaponBase _weapon;

        public WeaponBase CurrentWeapon => _weapon;
        
        public BattleUnit Owner { get; protected set; }

        public event Action<WeaponBase> OnWeaponChanged; 

        protected virtual void OnValidate()
        {
            var unit = GetComponentInParent<BattleUnit>();
            if (unit != null)
            {
                var weapons = unit.GetComponentsInChildren<WeaponBase>(true);
                _possibleWeapons = weapons.ToList();
            }
        }

        public virtual void Shot()
        {
            if (_weapon == null)
            {
                Debug.LogError($"{nameof(WeaponBase)} is missing!");
                return;
            }
            
            _weapon.Shot();
        }
        
        public void InitOwner(BattleUnit owner)
        {
            this.Owner = owner;
        }

        public void SetWeapon(WeaponBase weapon)
        {
            Assert.IsNotNull(weapon, "weapon != null");

            if (_weapon != null)
            {
                _weapon.gameObject.SetActive(false);
            }
            
            _weapon = weapon;
            _weapon.gameObject.SetActive(true);
            
            OnWeaponChanged?.Invoke(_weapon);
        }
        
    }
}
