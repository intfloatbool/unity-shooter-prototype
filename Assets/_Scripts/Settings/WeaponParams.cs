using _Scripts.Structs;
using UnityEngine;

namespace _Scripts.Settings
{
    [CreateAssetMenu(fileName = "WeaponParams", menuName = "BattleSettings/WeaponParams")]
    public class WeaponParams : ScriptableObject
    {
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

        [SerializeField] private Sprite _spriteIcon;
        public Sprite spriteIcon => _spriteIcon;
    }
}