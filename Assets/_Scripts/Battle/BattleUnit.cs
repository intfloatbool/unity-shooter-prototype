using _Scripts.Battle.Weapons;
using UnityEngine;

namespace _Scripts.Battle
{
    public class BattleUnit : MonoBehaviour
    {
        // Simulate photon using :-)
        [SerializeField] private bool _isLocalPlayer;
        public bool IsLocalPlayer => _isLocalPlayer;
        
        [SerializeField] private WeaponControllerBase _weaponController;
        public WeaponControllerBase WeaponController => _weaponController;

        [SerializeField] private HittableObject _hittableObject;
        public HittableObject HittableObject => _hittableObject;
        
        private void OnValidate()
        {
            if (_weaponController == null)
            {
                _weaponController = GetComponentInChildren<NullWeaponController>();
            }
            
            if (_hittableObject == null)
            {
                _hittableObject = GetComponentInChildren<HittableObject>();
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
