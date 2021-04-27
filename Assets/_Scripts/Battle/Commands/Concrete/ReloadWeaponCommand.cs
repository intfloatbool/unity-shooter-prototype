using _Scripts.Battle.Weapons;
using UnityEngine.Assertions;

namespace _Scripts.Battle.Commands.Concrete
{
    public class ReloadWeaponCommand : UnitCommandBase
    {
        public ReloadWeaponCommand(BattleUnit unit) : base(unit)
        {
        }

        public override void Execute()
        {
            Assert.IsNotNull(_receiverUnit.WeaponController, "_receiverUnit.WeaponController != null");
            var weapon = _receiverUnit.WeaponController.CurrentWeapon;
            if (weapon is FirearmWeapon firearmWeapon )
            {
                firearmWeapon.ReloadWeapon();
            }
        }
    }
}