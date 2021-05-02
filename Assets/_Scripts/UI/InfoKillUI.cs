using _Scripts.Battle;
using _Scripts.Static;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class InfoKillUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _killerNameText;
        [SerializeField] private TextMeshProUGUI _killedVictimNameText;

        [SerializeField] private float _dissapearTime = 1.5f;
        
        
        
        private void Awake()
        {
            GameHelper.CheckForNull(_killerNameText, this);
            GameHelper.CheckForNull(_killedVictimNameText, this);
        }

        private void OnEnable()
        {
            Invoke(nameof(HideUi), _dissapearTime);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(HideUi));
        }

        private void HideUi()
        {
            gameObject.SetActive(false);   
        }

        public void Init(BattleUnit killer, BattleUnit killedVictim)
        {
            _killerNameText.text = killer.name;
            _killerNameText.color = killer.TeamController.TeamColor;
            
            _killedVictimNameText.text = killedVictim.name;
            _killedVictimNameText.color = killedVictim.TeamController.TeamColor;
        }
    }
}
