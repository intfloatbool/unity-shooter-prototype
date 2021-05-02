using UnityEngine;

namespace _Scripts.Settings
{

    [System.Serializable]
    public struct PlayerCharacteristicsData
    {
        [Range(0, 100f)]
        [SerializeField] private float _moveSpeedMultipler;
        public float moveSpeedMultipler => _moveSpeedMultipler;
        
        [Range(0, 100f)]
        [SerializeField] private float _attackSpeedMultipler;
        public float attackSpeedMultipler => _attackSpeedMultipler;
        
        [Range(0, 100f)]
        [SerializeField] private float _damageDealingMultipler;
        public float damageDealingMultipler => _damageDealingMultipler;
        
        [Range(0, 100f)]
        [SerializeField] private float _damageTakingMultipler;
        public float damageTakingMultipler => _damageTakingMultipler;
        
        [Range(0, 100f)]
        [SerializeField] private float _damageScatterMultipler;
        public float damageScatterMultipler => _damageScatterMultipler;

        public PlayerCharacteristicsData(float moveSpeedMultipler, float attackSpeedMultipler,
            float damageDealingMultipler, float damageTakingMultipler, float damageScatterMultipler)
        {
            this._moveSpeedMultipler = moveSpeedMultipler;
            this._attackSpeedMultipler = attackSpeedMultipler;
            this._damageDealingMultipler = damageDealingMultipler;
            this._damageTakingMultipler = damageTakingMultipler;
            this._damageScatterMultipler = damageDealingMultipler;
        }
    }
    

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