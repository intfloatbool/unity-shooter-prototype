using System.Collections;
using _Scripts.Battle.Weapons;
using _Scripts.Static;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Battle
{
    public class BattleUnit : MonoBehaviour
    {
        // Simulate photon using :-)
        [SerializeField] private bool _isLocalPlayer;
        public bool IsLocalPlayer => _isLocalPlayer;

        private PlayerCamera _playerCamera;

        public PlayerCamera PlayerCamera
        {
            get
            {
                if (!_isLocalPlayer)
                {
                    Debug.LogError($"Trying to access camera from NOT LOCAL PLAYER!");
                    return null;
                }

                return _playerCamera;
            }
            set
            {
                _playerCamera = value;
            }
        }
        
        [SerializeField] private WeaponControllerBase _weaponController;
        public WeaponControllerBase WeaponController => _weaponController;

        [SerializeField] private HittableObject _hittableObject;
        public HittableObject HittableObject => _hittableObject;

        [SerializeField] private TeamController _teamController;
        public TeamController TeamController => _teamController;
        
        public BattleResources? BattleResources { get; set; }
        
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

        public void SetWeaponController(WeaponControllerBase weaponControllerBase, float? delay = null)
        {
            if (delay.HasValue)
            {
                StartCoroutine(SetWeaponControllerByDelay(weaponControllerBase, delay.Value));
            }
            else
            {
                _weaponController = weaponControllerBase;   
            }
        }

        private IEnumerator SetWeaponControllerByDelay(WeaponControllerBase weaponControllerBase, float delay)
        {
            yield return new WaitForSeconds(delay);
            _weaponController = weaponControllerBase;
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
