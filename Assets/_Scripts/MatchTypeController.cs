using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Static;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts
{
    public class MatchTypeController : MonoBehaviour
    {
        [SerializeField] private InitialSpawnCaller _spawnCaller;
        [SerializeField] private UnitsHolder _unitsHolder;
        [SerializeField] private MatchType _defaultMatchType = MatchType.FREE_FOR_ALL;
        
        [Space]
        [Header("Runtime")]
        [SerializeField] private MatchType _currentMatchType;
        private void Awake()
        {
            Assert.IsNotNull(_spawnCaller, "_spawnCaller != null");
            
            _spawnCaller.OnAllUnitsSpawned += SpawnCallerOnAllUnitsSpawned;
            
            if (_defaultMatchType != MatchType.NONE)
            {
                _currentMatchType = _defaultMatchType;
            }
        }

        private void OnDestroy()
        {
            if (_spawnCaller != null)
            {
                _spawnCaller.OnAllUnitsSpawned -= SpawnCallerOnAllUnitsSpawned;
            }
        }

        public void SetMatchTypeToTDM()
        {
            SetCurrentMatchType(MatchType.TEAM_DEATHMATCH);
        }
        
        public void SetMatchTypeToFFA()
        {
            SetCurrentMatchType(MatchType.FREE_FOR_ALL);
        }
        
        public void SetCurrentMatchType(MatchType matchType)
        {
            this._currentMatchType = matchType;
        }
        
        private void SpawnCallerOnAllUnitsSpawned()
        {
            InitTeams(_currentMatchType);
        }

        public void InitTeams(MatchType matchType)
        {
            Stack<TeamType> teamTypes = 
                new Stack<TeamType>(GameHelper.PlayableTeamTypes);

            int counter = 0;
            foreach (var unitInMatch in _unitsHolder.AliveUnits)
            {
                TeamType unitTeam = TeamType.NONE;
                
                if (matchType == MatchType.FREE_FOR_ALL)
                {
                    
                    if (!teamTypes.Any())
                    {
                        Debug.LogError($"Not enough teamTypes to init team!");
                        break;
                    }
                    
                    unitTeam = teamTypes.Pop();
                }
                else if (matchType == MatchType.TEAM_DEATHMATCH)
                {
                    unitTeam = (counter + 1) % 2 == 0 ? TeamType.TEAM_1 : TeamType.TEAM_2;
                }

                unitInMatch.TeamController.SetTeam(unitTeam);
                
                counter++;
            }

        }
    }
}
