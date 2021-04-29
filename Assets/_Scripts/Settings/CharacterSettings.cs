using UnityEngine;

namespace _Scripts.Settings
{

    [CreateAssetMenu(fileName = "CharacterSettings", menuName = "BattleSettings/CharacterSettings", order = 0)]
    public class CharacterSettings : ScriptableObject
    {
        [Range(0, 100f)]
        [SerializeField] private float _moveSpeedMultipler = 1;
        public float moveSpeedMultipler => _moveSpeedMultipler;
        
        [Range(0, 100f)]
        [SerializeField] private float _attackSpeedMultipler = 1;
        public float attackSpeedMultipler => _attackSpeedMultipler;
        
        [Range(0, 100f)]
        [SerializeField] private float _damageDealingMultipler = 1;
        public float damageDealingMultipler => _damageDealingMultipler;
        
        [Range(0, 100f)]
        [SerializeField] private float _damageTakingMultipler = 1;
        public float damageTakingMultipler => _damageTakingMultipler;
        
        [Range(0, 100f)]
        [SerializeField] private float _damageScatterMultipler = 1;
        public float damageScatterMultipler => _damageScatterMultipler;
    }
}