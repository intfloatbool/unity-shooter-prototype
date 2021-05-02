using System.Collections.Generic;
using _Scripts.Battle.Abilities;
using UnityEngine;

namespace _Scripts.Settings
{

    public class PassiveAbilityContainer
    {
        public string Name { get; private set; }
        
        private int _maxStacks;
        public int MaxStacks => _maxStacks;

        private PassiveAbilityData[] _passiveAbilityStats;
        public IReadOnlyCollection<PassiveAbilityData> PassiveAbilityStats => _passiveAbilityStats;

        public PassiveAbilityContainer(string name, int maxStacks, PassiveAbilityData[] abilityData)
        {
            Name = name;
            _maxStacks = maxStacks;
            _passiveAbilityStats = abilityData;
        }
    }
    
    [CreateAssetMenu(fileName = "PassiveAbilityParams", menuName = "BattleSettings/PassiveAbilityParams", order = 0)]
    public class PassiveAbilityParams : ScriptableObject
    {
        [SerializeField] private int _maxStacks;
        public int MaxStacks => _maxStacks;

        [SerializeField] private PassiveAbilityData[] _passiveAbilityStats;
        public IReadOnlyCollection<PassiveAbilityData> PassiveAbilityStats => _passiveAbilityStats;

        public PassiveAbilityContainer ToAbilityContainer() => 
            new PassiveAbilityContainer(
                this.name,
                _maxStacks,
                _passiveAbilityStats
        );
    }
    
}