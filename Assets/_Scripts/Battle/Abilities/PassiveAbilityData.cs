using UnityEngine;

namespace _Scripts.Battle.Abilities
{
    [System.Serializable]
    public struct PassiveAbilityData
    {
        [SerializeField] private ModificateType _modificateType;
        public ModificateType ModificateType => _modificateType;

        [SerializeField] private CharacteristcType _characteristcType;
        public CharacteristcType CharacteristcType => _characteristcType;

        [SerializeField] private float _value;
        public float Value => _value;
    }
}
