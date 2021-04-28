using _Scripts.Battle.Weapons;
using UnityEngine;
using UnityEngine.Assertions;

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

        [SerializeField] private TeamController _teamController;
        public TeamController TeamController => _teamController;
        
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
            
            if (_teamController == null)
            {
                _teamController = GetComponentInChildren<TeamController>();
            }
        }

        private void Awake()
        {
            if (_weaponController == null)
            {
                _weaponController = FindObjectOfType<NullWeaponController>();
            }
            
            Assert.IsNotNull(_weaponController, "_weaponController != null");
            Assert.IsNotNull(_hittableObject, "_hittableObject != null");
            Assert.IsNotNull(_teamController, "_teamController != null");
            
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
