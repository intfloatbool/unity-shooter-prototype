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
