using System;
using _Scripts.Enums;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts
{
    public abstract class TeamChangerListenerBase : MonoBehaviour
    {
        [SerializeField] protected TeamController _teamController;

        protected virtual void Awake()
        {
            if (_teamController != null)
            {
                _teamController.OnTeamChanged += TeamControllerOnTeamChanged;
                TeamControllerOnTeamChanged(_teamController.TeamType);
            }
        }

        public void UpdateTeamChanger(TeamController teamController)
        {
            if(_teamController != null)
                _teamController.OnTeamChanged -= TeamControllerOnTeamChanged;
            
            Assert.IsNotNull(teamController, "teamController != null");
            _teamController = teamController;
            _teamController.OnTeamChanged += TeamControllerOnTeamChanged;

            TeamControllerOnTeamChanged(teamController.TeamType);
        }
        

        protected virtual void OnDestroy()
        {
            if(_teamController != null)
                _teamController.OnTeamChanged -= TeamControllerOnTeamChanged;
        }

        protected abstract void TeamControllerOnTeamChanged(TeamType teamType);
    }
}
