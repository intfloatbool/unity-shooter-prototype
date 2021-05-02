using _Scripts.Enums;
using _Scripts.Settings;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _Scripts
{
    public class ImageColorTeamChangerListener : TeamChangerListenerBase
    {
        [SerializeField] private TeamColorsParams _teamColorsParams;
        [SerializeField] private Image[] _images;

        protected override void Awake()
        {
            Assert.IsNotNull(_teamColorsParams, "_teamColorsParams != null");
            base.Awake();
        }

        protected override void TeamControllerOnTeamChanged(TeamType teamType)
        {
            var color = _teamColorsParams.GetColorByTeam(teamType).Color;
            foreach (var img in _images)
            {
                if (img != null)
                {
                    img.color = color;
                }
            }

            _teamController.TeamColor = color;
        }
    }
}
