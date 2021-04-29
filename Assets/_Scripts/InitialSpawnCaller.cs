using System;
using _Scripts.Battle;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts
{
    /// <summary>
    /// Simulate room connection 
    /// </summary>
    public class InitialSpawnCaller : MonoBehaviour
    {
        [SerializeField] private UnitSpawner _spawner;
        
        [Space]
        [SerializeField] private BattleUnit[] _unitPrefabsToSpawn;

        public event Action OnAllUnitsSpawned; 

        private void Start()
        {
            Assert.IsNotNull(_spawner, "_spawner != null");
        }

        public void StartFillingRoom()
        {
            foreach (var unitPrefab in _unitPrefabsToSpawn)
            {
                if (unitPrefab != null)
                {
                    _spawner.Spawn(unitPrefab);
                }        
            }
            
            OnAllUnitsSpawned?.Invoke();
        }
    }
}
