using UnityEngine;

namespace _Scripts.Battle.Weapons
{
    public class StandartWeaponController : WeaponControllerBase
    {
        public override void Shot()
        {
            int damage = 0;
            if (_weapon is FirearmWeapon firearmWeapon)
            {
                damage = firearmWeapon.weaponParams.damageData.CalculateRandomDamage();
            }

            damage = Mathf.RoundToInt(((float) damage) * DamageMultipler);

            float initialDamage = damage;
            float scatteredDamage = initialDamage * DamageScatterMultipler;
            float min = Mathf.Min(scatteredDamage, initialDamage);
            float max = Mathf.Max(scatteredDamage, initialDamage);

            damage = Mathf.RoundToInt(Random.Range(min, max));

            _weapon.UpdateHitData(
                new HitData(
                    Owner,
                    _weapon,
                    damage
                    )
                );
            
            base.Shot();
        }
    }
}
