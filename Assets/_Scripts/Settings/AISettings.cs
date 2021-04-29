using UnityEngine;

namespace _Scripts.Settings
{
    [CreateAssetMenu(fileName = "AISettings", menuName = "BattleSettings/AISettings", order = 0)]
    public class AISettings : ScriptableObject
    {
        [SerializeField] private float _fireDelay = 1f;
        public float FireDelay => _fireDelay;

        [SerializeField] private float _distanceToStartAttack = 1.5f;
        public float DistanceToStartAttack => _distanceToStartAttack;

        [SerializeField] private Vector3 _minOffsetOfFire;
        [SerializeField] private Vector3 _maxOffsetOfFire;

        public Vector3 CalculateFireOffset()
        {
            return new Vector3(
                Random.Range(_minOffsetOfFire.x, _maxOffsetOfFire.x),
                Random.Range(_minOffsetOfFire.y, _maxOffsetOfFire.y),
                Random.Range(_minOffsetOfFire.z, _maxOffsetOfFire.z)
            );
        }
    }
}