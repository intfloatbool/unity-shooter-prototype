using System.Collections.Generic;
using _Scripts.Battle;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts
{
    public class UnitsHolder : MonoBehaviour
    {
        [SerializeField] private UnitSpawner _unitSpawner;

        private LinkedList<BattleUnit> _aliveUnits;
        
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
            
            _unitSpawner.OnUnitSpawned += UnitSpawnerOnUnitSpawned;
        }

        private void OnDestroy()
        {
            if(_unitSpawner != null)
                _unitSpawner.OnUnitSpawned -= UnitSpawnerOnUnitSpawned;
        }

        private void UnitSpawnerOnUnitSpawned(BattleUnit battleUnit)
        {
            Assert.IsNotNull(battleUnit, "battleUnit != null");
            if (battleUnit.HittableObject != null)
            {
                battleUnit.HittableObject.OnDied += () =>
                {
                    _aliveUnits.Remove(battleUnit);
                };
            }
            _aliveUnits.AddLast(battleUnit);
        }
    }
}
