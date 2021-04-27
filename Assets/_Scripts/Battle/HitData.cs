using _Scripts.Battle.Weapons;

namespace _Scripts.Battle
{
    public readonly struct HitData
    {
        public readonly BattleUnit unitSource;
        public readonly WeaponBase weaponSource;
        public readonly int damage;
        
        public HitData(BattleUnit unitSource, WeaponBase weaponSource, int damage)
        {
            this.unitSource = unitSource;
            this.weaponSource = weaponSource;
            this.damage = damage;
        }
    }
}
