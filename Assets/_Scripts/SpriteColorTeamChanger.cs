using _Scripts.Enums;
using UnityEngine;

namespace _Scripts
{
    public class SpriteColorTeamChanger : TeamChangerListenerBase
    {
        [System.Serializable]
        private struct ColorByTeam
        {
            public TeamType TeamType;
            public Color Color;
        }
        
        [SerializeField] private SpriteRenderer[] _spriteRenderers;
        [SerializeField] private ColorByTeam[] _colorByTeams;
        [SerializeField] private TeamType _defaultTeam;

        protected override void Awake()
        {
            TeamControllerOnTeamChanged(_defaultTeam);
            base.Awake();
        }

        protected override void TeamControllerOnTeamChanged(TeamType teamType)
        {
            var colorByTeam = GetColorByTeam(teamType);
            foreach (var spriteRend in _spriteRenderers)
            {
                if (spriteRend != null)
                {
                    spriteRend.color = colorByTeam.Color;
                }
            }
        }

        private ColorByTeam GetColorByTeam(TeamType teamType)
        {
            for (int i = 0; i < _colorByTeams.Length; i++)
            {
                if (_colorByTeams[i].TeamType == teamType)
                    return _colorByTeams[i];
            }

            Debug.LogError($"Color by team {teamType} is not found!");
            return new ColorByTeam
            {
                Color = Color.clear
            };
        } 
    }
}
