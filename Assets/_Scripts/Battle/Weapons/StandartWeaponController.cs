namespace _Scripts.Battle.Weapons
{
    public class StandartWeaponController : WeaponControllerBase
    {
        public override void Shot()
        {
            //TODO: Calculate damage by WeaponSettings
            int damage = int.MaxValue;
            
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
