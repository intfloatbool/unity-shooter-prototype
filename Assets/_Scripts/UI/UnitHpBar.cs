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
            _fillImg.fillAmount = 1;
        }

        public void InitOwner(BattleUnit owner)
        {
            if (this.Owner != null)
            {
                Owner.HittableObject.OnDamaged -= HittableObjectOnDamaged;
            }
            
            this.Owner = owner;
            Assert.IsNotNull(Owner, "Owner != null");
            Assert.IsNotNull(Owner.HittableObject, "owner.HittableObject != null");
            Owner.HittableObject.OnDamaged += HittableObjectOnDamaged;
            
            _fillImg.fillAmount = 1;
        }

        private void HittableObjectOnDamaged(AliveData aliveData, HitData hitData)
        {
            var maxHp = (float) aliveData.MaxHp;
            var currentHp = (float) aliveData.CurrentHp;
            var hpValue = currentHp / maxHp;

            _fillImg.fillAmount = hpValue;
        }
    }
}
