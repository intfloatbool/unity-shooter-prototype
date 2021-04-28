using _Scripts.Enums;
using _Scripts.Settings;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts
{
    public class SpriteColorTeamChanger : TeamChangerListenerBase
    {

        [SerializeField] private TeamColorsParams _teamColorsParams;
        [SerializeField] private SpriteRenderer[] _spriteRenderers;

        protected override void Awake()
        {
            Assert.IsNotNull(_teamColorsParams, "_teamColorsParams != null");
            TeamControllerOnTeamChanged(_teamController.TeamType);
            base.Awake();
        }

        protected override void TeamControllerOnTeamChanged(TeamType teamType)
        {
            var colorByTeam = _teamColorsParams.GetColorByTeam(teamType);
            foreach (var spriteRend in _spriteRenderers)
            {
                if (spriteRend != null)
                {
                    spriteRend.color = colorByTeam.Color;
                }
            }
        }

        
    }
}
