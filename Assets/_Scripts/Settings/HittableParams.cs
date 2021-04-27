using UnityEngine;

namespace _Scripts.Settings
{
    [CreateAssetMenu(fileName = "HittableParams", menuName = "BattleSettings/HittableParams")]
    public class HittableParams : ScriptableObject
    {
        [SerializeField] private int _maxHp = 100;
        public int MaxHp => _maxHp;
    }
}