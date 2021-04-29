using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Settings
{
    [CreateAssetMenu(fileName = "ShotSlowModiferSettings", menuName = "BattleSettings/ShotSlowModiferSettings", order = 0)]
    public class ShotSlowModiferSettings : ScriptableObject
    {
        [System.Serializable]
        private struct SpeedMultiplerByWeaponType
        {
            [Range(0f, 10f)]
            [SerializeField] private float _speedMultipler;
            public float SpeedMultipler => _speedMultipler;

            [SerializeField] private WeaponType _weaponType;
            public WeaponType WeaponType => _weaponType;
        }

        [SerializeField] private float _slowTime = 0.6f;
        public float slowTime => _slowTime;
        
        [SerializeField] private SpeedMultiplerByWeaponType[] _speedMultiplerCollection;

        public float GetSpeedMultiplerByWeaponType(WeaponType weaponType)
        {
            float? speedMultipler = default;
            foreach (var speedData in _speedMultiplerCollection)
            {
                if (speedData.WeaponType == weaponType)
                {
                    speedMultipler = speedData.SpeedMultipler;
                    break;
                }
            }

            if (!speedMultipler.HasValue)
            {
                Debug.LogError($"Speed multipler for weapon type {weaponType} is not found!");
                speedMultipler = 1f;
            }

            return speedMultipler.Value;
        }
    }
}