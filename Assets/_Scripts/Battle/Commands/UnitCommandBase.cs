
using UnityEngine.Assertions;

namespace _Scripts.Battle.Commands
{
    public abstract class UnitCommandBase
    {
        protected BattleUnit _receiverUnit;
        public UnitCommandBase(BattleUnit unit)
        {
            Assert.IsNotNull(unit, "unit != null");
            this._receiverUnit = unit;
        }

        public abstract void Execute();
    }
}
