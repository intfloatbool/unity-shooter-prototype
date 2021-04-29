using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Static;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace _Scripts.Battle
{
    public struct UnitSpawnData
    {
        public readonly BattleUnit OriginalPrefab;
        public readonly UnitSpawner UnitSpawner;
        public readonly Transform SpawnPoint;
        public readonly TeamType TeamType;   
        
        public UnitSpawnData(BattleUnit originalPrefab, UnitSpawner unitSpawner, Transform spawnPoint, TeamType teamType)
        {
            this.OriginalPrefab = originalPrefab;
            this.UnitSpawner = unitSpawner;
            this.SpawnPoint = spawnPoint;
            this.TeamType = teamType;
        }
    }

    public struct BattleResources
    {
        public readonly UnitsHolder UnitsHolder;

        public BattleResources(UnitsHolder unitsHolder)
        {
            UnitsHolder = unitsHolder;
        }
    }
    
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private UnitsHolder _unitsHolder;
        [SerializeField] private SpawnPoint[] _spawnPoints;
        [SerializeField] private bool _isExcludeSpawnPoints = true;
        
        private LinkedList<SpawnPoint> _spawnPointList;
        public event Action<BattleUnit> OnSpawned;

        private void OnValidate()
        {
            _spawnPoints = FindObjectsOfType<SpawnPoint>();
        }

        private void Awake()
        {
            GameHelper.CheckForNull(_unitsHolder);
            
        }

        public void StartRespawnProcess(BattleUnit initiator, UnitSpawnData spawnData, float? specificTime = null)
        {
            initiator.gameObject.SetActive(false);

            var spawnDataWithActualParams = new UnitSpawnData(
                spawnData.OriginalPrefab,
                spawnData.UnitSpawner,
                spawnData.SpawnPoint,
                initiator.TeamController.TeamType
            );
            
            if (specificTime.HasValue)
            {
                StartCoroutine(RespawnUnitByTime(initiator, spawnDataWithActualParams, specificTime.Value));
            }
            else
            {
                RespawnUnit(initiator, spawnDataWithActualParams);
            }
        }

        private IEnumerator RespawnUnitByTime(BattleUnit initiator, UnitSpawnData spawnData, float time)
        {
            yield return new WaitForSeconds(time);
            RespawnUnit(initiator, spawnData);
        }

        private void RespawnUnit(BattleUnit initiator, UnitSpawnData spawnData)
        {
            Spawn(spawnData.OriginalPrefab, spawnData);
            Destroy(initiator.gameObject);
        }

        public void Spawn(BattleUnit unitPrefab, UnitSpawnData? spawnData = null)
        {
            Transform spawnPoint = null;
            Vector3 spawnPosition = Vector3.zero;
            if (spawnData.HasValue)
            {
                spawnPoint = spawnData.Value.SpawnPoint;
                spawnPosition = spawnPoint.position;
            }
            else
            {
                if (_isExcludeSpawnPoints)
                {
                    if (_spawnPointList != null && !_spawnPointList.Any())
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
                    spawnPoint = randomPoint.transform;
                    _spawnPointList.Remove(randomPoint);

                }
                else
                {
                    spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform;
                    spawnPosition = spawnPoint.position;
                }
            }
            

            var unitInstance = Instantiate(unitPrefab);
            unitInstance.transform.position = spawnPosition;

            var respawnableUnit = unitInstance.GetComponentInChildren<IRespawnableUnit>(true);
            if (respawnableUnit != null)
            {
                if (spawnData.HasValue)
                {
                    respawnableUnit.InitRespawnBehaviour(spawnData.Value);
                    
                    unitInstance.TeamController.SetTeam(spawnData.Value.TeamType);
                }
                else
                {
                    respawnableUnit.InitRespawnBehaviour(new UnitSpawnData(
                        unitPrefab, this, spawnPoint, TeamType.TEAM_1
                    ));
                }
                
            }

            InitUnit(unitInstance);
            
            OnSpawned?.Invoke(unitInstance);
        }

        private void InitUnit(BattleUnit battleUnit)
        {
            var battleResources = new BattleResources(
                _unitsHolder
            );
            battleUnit.BattleResources = battleResources;
        }
    }
}
