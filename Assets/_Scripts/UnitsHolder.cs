using System.Collections.Generic;
using System.Linq;
using _Scripts.Battle;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts
{
    public class UnitsHolder : MonoBehaviour
    {
        [SerializeField] private UnitSpawner _unitSpawner;

        private LinkedList<BattleUnit> _aliveUnits;
        public IReadOnlyCollection<BattleUnit> AliveUnits => _aliveUnits;
        
#if UNITY_EDITOR
        [Space]
        [Header("Debug")]
        [SerializeField] private List<BattleUnit> _currentUnitsEditor;
#endif
        
        private void OnValidate()
        {
            if (_unitSpawner == null)
            {
                _unitSpawner = FindObjectOfType<UnitSpawner>();
            }
        }

        private void Awake()
        {
            Assert.IsNotNull(_unitSpawner, "_unitSpawner != null");

            _aliveUnits = new LinkedList<BattleUnit>();
            
            _unitSpawner.OnSpawned += SpawnerOnSpawned;
        }

        private void OnDestroy()
        {
            if(_unitSpawner != null)
                _unitSpawner.OnSpawned -= SpawnerOnSpawned;
        }

        private void SpawnerOnSpawned(BattleUnit battleUnit)
        {
            Assert.IsNotNull(battleUnit, "battleUnit != null");
            if (battleUnit.HittableObject != null)
            {
                battleUnit.HittableObject.OnDied += () =>
                {
                    _aliveUnits.Remove(battleUnit);
                    UpdateDebugEditorList();
                };
            }
            _aliveUnits.AddLast(battleUnit);
            UpdateDebugEditorList();
        }

        private void UpdateDebugEditorList()
        {
#if UNITY_EDITOR
            _currentUnitsEditor = _aliveUnits.ToList();
#endif
        }
    }
}
