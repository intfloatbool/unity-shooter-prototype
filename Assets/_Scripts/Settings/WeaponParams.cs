using _Scripts.Enums;
using _Scripts.Static;
using _Scripts.Structs;
using UnityEngine;

namespace _Scripts.Settings
{
    [CreateAssetMenu(fileName = "WeaponParams", menuName = "BattleSettings/WeaponParams")]
    public class WeaponParams : ScriptableObject
    {
        [SerializeField] private WeaponType _weaponType;
        public WeaponType WeaponType => _weaponType;
        
        [SerializeField] private LayerMask _hitLayer;
        public LayerMask HitLayer => _hitLayer;

        [SerializeField] private GameObject _projectilePrefab;

        public GameObject ProjectilePrefab
        {
            get
            {
                if (_projectilePrefab == null)
                {
                    return GameHelper.NullObjects.NullGameObject;
                }

                return _projectilePrefab;
            }
        }
        
        [SerializeField] private int _magazineAmount = 10;
        public int magazineAmount => _magazineAmount;
        
        [SerializeField] private string _nameId;
        public string nameId => _nameId;

        [SerializeField] private float _delay;
        public float delay => _delay;

        [SerializeField] private float _reloadTime;
        public float reloadTime => _reloadTime;

        [SerializeField] private DamageData _damageData;
        public DamageData damageData => _damageData;

        [Range(0, 100)]
        [SerializeField] private int _projectilesPerShot;
        public int projectilesPerShot => _projectilesPerShot;

        [SerializeField] private Vector3 _projectilesOffset;
        public Vector3 projectilesOffset => _projectilesOffset;

        [SerializeField] private Sprite _spriteIcon;
        public Sprite spriteIcon => _spriteIcon;
    }
}