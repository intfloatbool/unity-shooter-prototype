using _Scripts.Battle.Weapons;
using UnityEngine;

namespace _Scripts.Battle
{
    public class BattleUnit : MonoBehaviour
    {
        [SerializeField] private WeaponControllerBase _weaponController;
        public WeaponControllerBase WeaponController => _weaponController;

        private void OnValidate()
        {
            if (_weaponController == null)
            {
                _weaponController = FindObjectOfType<NullWeaponController>();
            }
        }

        private void Awake()
        {
            if (_weaponController == null)
            {
                _weaponController = FindObjectOfType<NullWeaponController>();
            }
            
            InitOwnerableChilds();
        }

        private void InitOwnerableChilds()
        {
            var ownerableChilds = GetComponentsInChildren<IOwnerable>();
            foreach (var ownerable in ownerableChilds)
            {
                ownerable.InitOwner(this);
            }
        }
        
    }
}
