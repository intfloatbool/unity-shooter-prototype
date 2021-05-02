using System.Collections.Generic;
using _Scripts.Battle.Base;
using _Scripts.Battle.Setupable;
using _Scripts.Settings;
using _Scripts.Static;
using UnityEngine;

namespace _Scripts.Battle.Abilities
{
    public class UnitAbilityStatusController : MonoBehaviour
    {
        [SerializeField] private CharacterSettingsAccepterBase _settingsAccepter;
        
        private Dictionary<string, Stack<PassiveAbilityContainer>> _currentAbilitiesDict =
            new Dictionary<string, Stack<PassiveAbilityContainer>>();

        private UnitSpeedControllerBase _unitSpeedController;

        private void OnValidate()
        {
            if (_settingsAccepter == null)
            {
                _settingsAccepter = GetComponentInChildren<CharacterSettingsAccepterBase>();
            }
        }

        private void Awake()
        {
            GameHelper.CheckForNull(_settingsAccepter, this);

            _unitSpeedController = GetComponentInChildren<UnitSpeedControllerBase>();
        }

        public void ApplyPassiveAbility(PassiveAbilityParams passiveAbilityParams)
        {
            var abilityContainer = passiveAbilityParams.ToAbilityContainer();
            if (_currentAbilitiesDict.ContainsKey(abilityContainer.Name))
            {
                var containers = _currentAbilitiesDict[abilityContainer.Name];
                
                if(containers.Count < passiveAbilityParams.MaxStacks)
                    _currentAbilitiesDict[abilityContainer.Name].Push(abilityContainer);
            }
            else
            {
                _currentAbilitiesDict.Add(abilityContainer.Name, new Stack<PassiveAbilityContainer>());
                _currentAbilitiesDict[abilityContainer.Name].Push(abilityContainer);
            }

            UpdateStatsByAbilitiesContainer();
        }

        public void ClearAbilities()
        {
            _currentAbilitiesDict.Clear();
            UpdateStatsByAbilitiesContainer();
        }

        private void UpdateStatsByAbilitiesContainer()
        {
            /*
             *      _characterSettings.moveSpeedMultipler,
                    _characterSettings.attackSpeedMultipler,
                    _characterSettings.damageDealingMultipler,
                    _characterSettings.damageTakingMultipler,
                    _characterSettings.damageScatterMultipler
             * 
             */

            _settingsAccepter.ResetSettings();

            var defaultData = _settingsAccepter.DefaultSettingsData.Value;
            float moveSpeedMultipler = defaultData.moveSpeedMultipler;
            float attackSpeedMultipler = defaultData.attackSpeedMultipler;
            float damageDealingMultipler = defaultData.damageDealingMultipler;
            float damageTakingMultipler = defaultData.damageTakingMultipler;
            float damageScatterMultipler = defaultData.damageScatterMultipler;

            foreach (var kvp in _currentAbilitiesDict)
            {
                foreach (var abilityContainer in kvp.Value)
                {
                    foreach (var abilityData in abilityContainer.PassiveAbilityStats)
                    {
                        var modType = abilityData.ModificateType;
                        var characteristicType = abilityData.CharacteristcType;

                        if (characteristicType == CharacteristcType.MOVE_SPEED_MULTIPLIER)
                        {
                            moveSpeedMultipler = CalculateValueByModificateType(modType, moveSpeedMultipler,
                                abilityData.Value);
                        }
                        else if (characteristicType == CharacteristcType.ATTACK_SPEED_MULTIPLER)
                        {
                            attackSpeedMultipler = CalculateValueByModificateType(modType, attackSpeedMultipler,
                                abilityData.Value);
                        }
                        else if (characteristicType == CharacteristcType.DAMAGE_DEALING_MULTIPLER)
                        {
                            damageDealingMultipler = CalculateValueByModificateType(modType, damageDealingMultipler,
                                abilityData.Value);
                        }
                        else if (characteristicType == CharacteristcType.DAMAGE_TAKING_MULTIPLER)
                        {
                            damageTakingMultipler = CalculateValueByModificateType(modType, damageTakingMultipler,
                                abilityData.Value);
                        }
                        else if (characteristicType == CharacteristcType.DAMAGE_SCATTER_MULTIPLER)
                        {
                            damageScatterMultipler = CalculateValueByModificateType(modType, damageScatterMultipler,
                                abilityData.Value);
                        }
                    }
                }
            }

            _settingsAccepter.AcceptSettings(
                new PlayerCharacteristicsData(
                    moveSpeedMultipler,
                    attackSpeedMultipler,
                    damageDealingMultipler,
                    damageTakingMultipler,
                    damageScatterMultipler
                )
            );

            if (_unitSpeedController != null)
            {
                _unitSpeedController.ResetSpeedToDefault();
            }

            Debug.Log($"Abilities accepted to unit {gameObject.name} amount: {_currentAbilitiesDict.Count}");
            if (_currentAbilitiesDict.Count > 0)
            {
                Debug.Log("List:");
            }
            foreach (var kvp in _currentAbilitiesDict)
            {
                Debug.Log($"Ability name: {kvp.Key}, stacks: {kvp.Value.Count}");
            }
        }

        private float CalculateValueByModificateType(ModificateType modType, float input, float value)
        {
            if (modType == ModificateType.ADDITIVE)
            {
                return input + value;
            }
            else if (modType == ModificateType.MULTIPLICATION)
            {
                return input * value;
            }
            else if (modType == ModificateType.MULTIP_SHARED)
            {
                return value < 0 ? input / value : input * value;
            }

            Debug.LogError($"Cannot calculate value by modType: " + modType);
            
            return Mathf.Infinity;
        }
    }
}
