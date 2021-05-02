using _Scripts.Battle.Base;
using _Scripts.Settings;
using _Scripts.Static;
using UnityEngine;

namespace _Scripts.Battle.Setupable
{
    [RequireComponent(typeof(BattleUnit))]
    public abstract class CharacterSettingsAccepterBase : MonoBehaviour, ISetupable
    {
        [SerializeField] protected BattleUnit _battleUnit;
        [SerializeField] protected CharacterSettings _characterSettings;

        protected PlayerCharacteristicsData? _defaultSettingsData;
        public PlayerCharacteristicsData? DefaultSettingsData => _defaultSettingsData;
        
        [SerializeField] protected PlayerCharacteristicsData _currentSettingsData;
        
        protected virtual void OnValidate()
        {
            if (_battleUnit == null)
            {
                _battleUnit = GetComponent<BattleUnit>();
            }
        }

        protected virtual void Awake()
        {
            GameHelper.CheckForNull(_battleUnit, this);
            GameHelper.CheckForNull(_characterSettings, this);
        }

        public abstract void AcceptSettings(PlayerCharacteristicsData data);
        
        public void Setup()
        {

            if (!_defaultSettingsData.HasValue)
            {
                _defaultSettingsData = new PlayerCharacteristicsData(
                    _characterSettings.moveSpeedMultipler,
                    _characterSettings.attackSpeedMultipler,
                    _characterSettings.damageDealingMultipler,
                    _characterSettings.damageTakingMultipler,
                    _characterSettings.damageScatterMultipler
                );
            }

            ResetSettings();
        }

        public void ResetSettings()
        {
            _currentSettingsData = _defaultSettingsData.Value;
            AcceptSettings(_currentSettingsData);
        }
    }
}