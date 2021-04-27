using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Static;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace _Scripts.Battle
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnPoint[] _spawnPoints;
        [SerializeField] private bool _isExcludeSpawnPoints = true;

        private LinkedList<SpawnPoint> _spawnPointList;
        
        public event Action<BattleUnit> OnUnitSpawned;

        private void OnValidate()
        {
            _spawnPoints = FindObjectsOfType<SpawnPoint>();
        }

        private void Awake()
        {
            Assert.IsTrue(_spawnPoints.Length > 0, "_spawnPoints.Length > 0");
            Assert.IsTrue(_spawnPoints.All(sp => sp != null), "_spawnPoints.All(sp => sp != null)");
        }

        public void SpawnUnit(BattleUnit unitPrefab)
        {
            Vector3 spawnPosition = Vector3.zero;
            if (_isExcludeSpawnPoints)
            {
                if (!_spawnPointList.Any())
                {
                    _spawnPointList = null;
                }
                
                if (_spawnPointList == null)
                {
                    var pointsCopy = _spawnPoints.ToList();
                    pointsCopy.Shuffle();
                    _spawnPointList = new LinkedList<SpawnPoint>(pointsCopy);
                }

                var randomPoint = _spawnPointList.LastOrDefault();
                
                Assert.IsNotNull(randomPoint, "randomPoint != null");
                spawnPosition = randomPoint.transform.position;
                _spawnPointList.Remove(randomPoint);

            }
            else
            {
                spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;
            }

            var unitInstance = Instantiate(unitPrefab);
            unitInstance.transform.position = spawnPosition;
            
            OnUnitSpawned?.Invoke(unitInstance);
        }
    }
}
