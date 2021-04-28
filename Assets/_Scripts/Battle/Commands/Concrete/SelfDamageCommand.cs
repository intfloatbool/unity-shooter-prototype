namespace _Scripts.Battle.Commands.Concrete
{
    public class SelfDamageCommand : UnitCommandBase
    {
        private int _dmg;
        public SelfDamageCommand(BattleUnit unit, int dmg) : base(unit)
        {
            this._dmg = dmg;
        }

        public override void Execute()
        {
            if (_receiverUnit.HittableObject != null)
            {
                _receiverUnit.HittableObject.SelfDamage(_dmg);
            }
        }
    }
}