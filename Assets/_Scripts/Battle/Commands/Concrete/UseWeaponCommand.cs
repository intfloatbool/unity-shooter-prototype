namespace _Scripts.Battle.Commands.Concrete
{
    public class UseWeaponCommand : UnitCommandBase
    {
        public UseWeaponCommand(BattleUnit unit) : base(unit)
        {
        }

        public override void Execute()
        {
            if (_receiverUnit != null && _receiverUnit.WeaponController != null)
            {
                _receiverUnit.WeaponController.Shot();
            } 
        }
    }
}
