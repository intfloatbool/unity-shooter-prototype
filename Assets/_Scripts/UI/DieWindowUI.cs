using System;
using _Scripts.Battle;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class DieWindowUI : MonoBehaviour, IOwnerable
    {
        [SerializeField] private Transform _layout;
        [SerializeField] private Button _respawnBtn;
        
        public BattleUnit Owner { get; private set; }

        private void Awake()
        {
            Assert.IsNotNull(_layout, "_layout != null");
            Assert.IsNotNull(_respawnBtn, "_respawnBtn != null");

            if (_respawnBtn != null)
            {
                _respawnBtn.onClick.AddListener(RespawnPlayer);
            }
        }

        public void InitOwner(BattleUnit owner)
        {
            if (this.Owner != null)
            {
                this.Owner.HittableObject.OnDied -= HittableObjectOnDied;
            }
            
            this.Owner = owner;

            this.Owner.HittableObject.OnDied += HittableObjectOnDied;

        }

        private void HittableObjectOnDied()
        {
            _layout.gameObject.SetActive(true);
        }


        private void RespawnPlayer()
        {
            var respawnComponent = Owner.GetComponentInChildren<OnDieRespawnableImmediately>(true);
            Assert.IsNotNull(respawnComponent, "respawnComponent != null");
            
            respawnComponent.RespawnNow();
            
            _layout.gameObject.SetActive(false);
        }
    }
}
