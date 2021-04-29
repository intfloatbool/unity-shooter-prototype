using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Battle;
using _Scripts.Enums;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts
{
    public class UnitsHolder : MonoBehaviour
    {
        [SerializeField] private UnitSpawner _unitSpawner;

        private LinkedList<BattleUnit> _aliveUnits;
        public IReadOnlyCollection<BattleUnit> AliveUnits => _aliveUnits;

        public BattleUnit[] AliveUnitsBuffer { get; private set; }
        
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

            var teamAmount = Enum.GetNames(typeof(TeamType)).Length;
            AliveUnitsBuffer = new BattleUnit[teamAmount];
            
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
            UpdateUnitsBuffer();
        }


        private void UpdateUnitsBuffer()
        {
            int counter = 0;
            foreach (var aliveUnit in _aliveUnits)
            {
                if (counter > AliveUnitsBuffer.Length - 1)
                {
                    break;
                }

                AliveUnitsBuffer[counter] = aliveUnit;
                
                counter++;
            }
        }

        private void UpdateDebugEditorList()
        {
#if UNITY_EDITOR
            _currentUnitsEditor = _aliveUnits.ToList();
#endif
        }
    }
}
