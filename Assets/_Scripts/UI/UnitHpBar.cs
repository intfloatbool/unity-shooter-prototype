using System;
using _Scripts.Battle;
using _Scripts.Structs;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UnitHpBar : MonoBehaviour, IOwnerable
    {
        [SerializeField] private Image _fillImg;
        public BattleUnit Owner { get; private set; }

        private void Awake()
        {
            Assert.IsNotNull(_fillImg, "_fillImg != null");
        }

        private void OnEnable()
        {
            _fillImg.fillAmount = 1;
        }

        public void InitOwner(BattleUnit owner)
        {
            if (this.Owner != null)
            {
                Owner.HittableObject.OnDamaged -= HittableObjectOnDamaged;
                owner.HittableObject.OnDied -= HittableObjectOnDied;
            }
            
            this.Owner = owner;
            Assert.IsNotNull(Owner, "Owner != null");
            Assert.IsNotNull(Owner.HittableObject, "owner.HittableObject != null");
            Owner.HittableObject.OnDamaged += HittableObjectOnDamaged;
            owner.HittableObject.OnDied += HittableObjectOnDied;
            
            _fillImg.fillAmount = 1;
            
            _fillImg.gameObject.SetActive(true);
        }

        private void HittableObjectOnDied()
        {
            _fillImg.gameObject.SetActive(false);
            _fillImg.fillAmount = 1;
        }

        private void HittableObjectOnDamaged(AliveData aliveData, HitData hitData)
        {
            _fillImg.gameObject.SetActive(true);
            var maxHp = (float) aliveData.MaxHp;
            var currentHp = (float) aliveData.CurrentHp;
            var hpValue = currentHp / maxHp;

            _fillImg.fillAmount = hpValue;
        }
    }
}
