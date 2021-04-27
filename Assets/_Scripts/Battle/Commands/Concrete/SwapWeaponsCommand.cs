using System.Linq;

namespace _Scripts.Battle.Commands.Concrete
{
    public class SwapWeaponsCommand : UnitCommandBase
    {
        public SwapWeaponsCommand(BattleUnit unit) : base(unit)
        {
        }

        protected override void Execute()
        {
            if (_receiverUnit == null)
                return;

            if (_receiverUnit.WeaponController == null)
                return;

            if (!_receiverUnit.WeaponController.PossibleWeapons.Any())
                return;
            
            var currentWeapon = _receiverUnit.WeaponController.CurrentWeapon;
            var possibleWeaponsArr = _receiverUnit.WeaponController.PossibleWeapons
                .ToArray();

            var currentWeaponIndex = -1;
            for (int i = 0; i < possibleWeaponsArr.Length; i++)
            {
                var possibleWeapon = possibleWeaponsArr[i];
                if (possibleWeapon.Equals(currentWeapon))
                {
                    currentWeaponIndex = i;
                    break;
                }
            }

            var nextIndex = currentWeaponIndex + 1;
            if (nextIndex > possibleWeaponsArr.Length - 1)
            {
                nextIndex = 0;
            }

            var nextWeapon = possibleWeaponsArr[nextIndex];
            
            _receiverUnit.WeaponController.SetWeapon(nextWeapon);
        }
    }
}
