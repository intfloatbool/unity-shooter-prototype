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

        public abstract void AcceptSettings();
        public void Setup()
        {
            AcceptSettings();
        }
    }
}