using System;
using _Scripts.Battle;
using _Scripts.Enums;
using UnityEngine;

namespace _Scripts
{
    public class TeamController : MonoBehaviour
    {
        [Space]
        [Header("Runtime")]
        [SerializeField] private TeamType _teamType;
        public TeamType TeamType => _teamType;

        public event Action<TeamType> OnTeamChanged; 
        
        public void SetTeam(TeamType teamType)
        {
            this._teamType = teamType;

            OnTeamChanged?.Invoke(_teamType);
        }

        public bool IsTeammate(BattleUnit anotherUnit)
        {
            if (anotherUnit.TeamController == null)
                return false;

            return _teamType == anotherUnit.TeamController.TeamType;
        }
        
        public bool IsTeammate(TeamController teamController)
        {
            if (teamController == null)
                return false;

            return _teamType == teamController.TeamType;
        }
    }
}
