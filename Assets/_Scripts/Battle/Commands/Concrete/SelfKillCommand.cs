namespace _Scripts.Battle.Commands.Concrete
{
    public class SelfKillCommand : UnitCommandBase
    {
        public SelfKillCommand(BattleUnit unit) : base(unit)
        {
        }

        public override void Execute()
        {
            var hittable = _receiverUnit.HittableObject;
            if (hittable != null)
            {
                hittable.Kill();
            }
        }
    }
}