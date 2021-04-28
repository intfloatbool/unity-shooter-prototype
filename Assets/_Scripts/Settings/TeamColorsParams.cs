using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.Settings
{
    [System.Serializable]
    public struct ColorByTeam
    {
        public TeamType TeamType;
        public Color Color;
    }
    
    [CreateAssetMenu(fileName = "TeamColorsParams", menuName = "BattleSettings/TeamColorsParams", order = 0)]
    public class TeamColorsParams : ScriptableObject
    {
        [SerializeField] private ColorByTeam[] _colorByTeams;
        
        public ColorByTeam GetColorByTeam(TeamType teamType)
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